using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace WindowsTime.Core.Monitorador.Helpers
{
    internal static class SerializationHelper
    {
        internal static void SerializeObject<T>(T serializableObject, string fileName) where T : class
        {
            var xmlDocument = new XmlDocument();
            var serializer = new XmlSerializer(serializableObject.GetType());
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(stream, serializableObject);
                stream.Position = 0;
                xmlDocument.Load(stream);
                xmlDocument.Save(fileName);
                stream.Close();
            }
        }

        internal static T DeSerializeObject<T>(string fileName)
        {
            try
            {
                var objectOut = default(T);

                var xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                string xmlString = xmlDocument.OuterXml;

                using (var read = new StringReader(xmlString))
                {
                    var outType = typeof(T);

                    var serializer = new XmlSerializer(outType);
                    using (var reader = new NamespaceIgnorantXmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }

                return objectOut;
            }
            catch (System.Exception)
            {
                return default(T);
            }
        }

        internal class NamespaceIgnorantXmlTextReader : XmlTextReader
        {
            public NamespaceIgnorantXmlTextReader(System.IO.TextReader reader) : base(reader) { }

            public override string NamespaceURI
            {
                get { return ""; }
            }
        }
    }
}
