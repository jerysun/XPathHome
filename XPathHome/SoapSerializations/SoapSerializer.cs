using System.Xml;
using System.Xml.Serialization;

namespace XPathHome.SoapSerializations
{
  public class SoapSerializer
  {
    public static void DoSerialization<T>(T t, string fileName)
    {
      XmlTypeMapping mapping = new SoapReflectionImporter().ImportTypeMapping(typeof(T));
      XmlSerializer serializer = new XmlSerializer(mapping);

      using (FileStream fs = new FileStream(fileName, FileMode.Create))
      {
        using (XmlWriter writer = XmlWriter.Create(fs))
        {
          writer.WriteStartElement("Root");
          serializer.Serialize(writer, t);
          writer.WriteEndElement();
        }
      }
    }
  }
}
