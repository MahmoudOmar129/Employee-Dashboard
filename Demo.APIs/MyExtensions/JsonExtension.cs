using Demo.DAL.Entity;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Serialization;
//using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Demo.APIs.MyExtensions
{
    public static partial class JsonExtension
    {

        public static string ConverToXml(object json)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Person));

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, json);
                return textWriter.ToString();
            }
        }

        //public static string ConvertToJson(Employee emp)
        //{
        //    return JsonSerializer.Serialize(emp);
        //}


        public static XmlDocument DeserializeXmlNode(string json, string rootName, string rootPropertyName)
        {
            return DeserializeXmlNode(new StringReader(json), rootName, rootPropertyName);
        }

        public static XmlDocument DeserializeXmlNode(TextReader textReader, string rootName, string rootPropertyName)
        {
            var prefix = "{" + JsonConvert.SerializeObject(rootPropertyName) + ":";
            var postfix = "}";

            using (var combinedReader = new StringReader(prefix).Concat(textReader).Concat(new StringReader(postfix)))
            {
                var settings = new JsonSerializerSettings
                {
                    Converters = { new Newtonsoft.Json.Converters.XmlNodeConverter() { DeserializeRootElementName = rootName } },
                    DateParseHandling = DateParseHandling.None,
                };
                using (var jsonReader = new JsonTextReader(combinedReader) { CloseInput = false, DateParseHandling = DateParseHandling.None })
                {
                    return JsonSerializer.CreateDefault(settings).Deserialize<XmlDocument>(jsonReader);
                }
            }
        }
    }

    public static class Extensions
    {
        public static TextReader Concat(this TextReader first, TextReader second)
        {
            return new ChainedTextReader(first, second);
        }

        private class ChainedTextReader : TextReader
        {
            private TextReader first;
            private TextReader second;
            private bool readFirst = true;

            public ChainedTextReader(TextReader first, TextReader second)
            {
                this.first = first;
                this.second = second;
            }

            public override int Peek()
            {
                if (readFirst)
                {
                    return first.Peek();
                }
                else
                {
                    return second.Peek();
                }
            }

            public override int Read()
            {
                if (readFirst)
                {
                    int value = first.Read();
                    if (value == -1)
                    {
                        readFirst = false;
                    }
                    else
                    {
                        return value;
                    }
                }
                return second.Read();
            }

            public override void Close()
            {
                first.Close();
                second.Close();
            }

            protected override void Dispose(bool disposing)
            {
                base.Dispose(disposing);
                if (disposing)
                {
                    first.Dispose();
                    second.Dispose();
                }
            }
        }
    }


}
