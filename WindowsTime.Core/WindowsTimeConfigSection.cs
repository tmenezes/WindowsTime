using System.Configuration;

namespace WindowsTime.Core
{
    public class WindowsTimeConfigSection : ConfigurationSection
    {
        public static WindowsTimeConfigSection Current
        {
            get
            {
                return ConfigurationManager.GetSection("WindowsTime") as WindowsTimeConfigSection;
            }
        }


        [ConfigurationProperty("enderecoDoServico", IsRequired = true)]
        public string EnderecoDoServico
        {
            get
            {
                return (string)this["enderecoDoServico"];
            }

            set
            {
                this["enderecoDoServico"] = value;
            }
        }
    }
}
