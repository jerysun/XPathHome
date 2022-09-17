using System.Xml.Serialization;

namespace XPathHome.Serializations
{
  public class SerializeXml
  {
    public static void DoSerialization<T>(T t, string fileName)
    {
      XmlSerializer serializer = new XmlSerializer(typeof(T), "");

      using (MemoryStream stream = new MemoryStream())
      {
        serializer.Serialize(stream, t);

        using(FileStream fs = new FileStream(fileName, FileMode.Create))
        {
          stream.WriteTo(fs);
          fs.Flush();
        }
      }
    }
  }
}
