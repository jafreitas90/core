using System.Collections.Generic;
using System.IO;
using System.Xml;
using ExtendedXmlSerializer;
using ExtendedXmlSerializer.Configuration;

namespace CurrencyConverter.Shared.Utilities
{
    public static class XMlUtil
    {
        static IExtendedXmlSerializer serializer;
        static XMlUtil()
        {
            serializer = new ConfigurationContainer().Create();
        }

        public static void Save(string file, Dictionary<string, string> keyValuePairs)
        {
            var xml = serializer.Serialize(
                new XmlWriterSettings { Indent = true },
                keyValuePairs);

            using (StreamWriter outputFile = new StreamWriter(file))
            {
                outputFile.WriteLine(xml);
            }
        }

        public static Dictionary<string, string> Read(string file)
        {
            if (File.Exists(file))
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    var text = sr.ReadToEnd();
                    return serializer.Deserialize<Dictionary<string, string>>(text);
                }
            }
            return null;
        }
    }
}
