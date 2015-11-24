using System;
using System.Reflection;
using System.Text;
using PostSharp.Aspects;
using PostSharp.Extensibility;

namespace WindowsTime.Infraestrutura.Aop
{
    /// <summary>
    /// Thread Safe Cache Aspect with optional expiration parameter
    /// </summary>
    [Serializable]
    public class CacheAspectAttribute : MethodInterceptionAspect
    {
        // fields
        [NonSerialized]
        private static readonly ICache _cache;
        [NonSerialized]
        private object syncRoot;
        private string _methodName;

        // properties
        public int ExpirationMinutes { get; set; }
        public bool CacheNullValues { get; set; }
        public static ICache Cache { get { return _cache; } }

        // constructor
        static CacheAspectAttribute()
        {
            if (!PostSharpEnvironment.IsPostSharpRunning)
            {
                _cache = new StaticInMemoryCache();
            }
        }
        public CacheAspectAttribute()
            : this(ExpirationConst.Minutes.Twenty)
        {
        }
        public CacheAspectAttribute(int expirationMinutes)
        {
            this.ExpirationMinutes = expirationMinutes;
            this.CacheNullValues = false;
        }


        // publics
        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            _methodName = method.Name;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            syncRoot = new object();
        }

        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var key = BuildCacheKey(args.Arguments);
            if (_cache.HasElement(key))
            {
                args.ReturnValue = _cache[key];
            }
            else
            {
                lock (syncRoot)
                {
                    if (!_cache.HasElement(key))
                    {
                        var returnVal = args.Invoke(args.Arguments);
                        args.ReturnValue = returnVal;

                        if (returnVal != null || this.CacheNullValues)
                            try
                            {
                                _cache.AddInCache(key, returnVal, this.ExpirationMinutes);
                            }
                            catch { }
                    }
                    else
                    {
                        args.ReturnValue = _cache[key];
                    }
                }
            }
        }

        private string BuildCacheKey(Arguments arguments)
        {
            var sb = new StringBuilder();
            sb.Append(_methodName);
            foreach (var argument in arguments.ToArray())
            {
                sb.Append((string)(argument == null ? "_" : argument.ToString()));
            }
            return sb.ToString();
        }
    }
}
