using System.Xml.Serialization;

namespace XPathHome.Deserializations
{
  public class DeserializeXml
  {
    public static void DoDeserialization<T>(out T? t, string fileName)
    {
      XmlSerializer serializer = new XmlSerializer(typeof(T));
      using (FileStream fs = new FileStream(fileName, FileMode.Open))
      {
        var ret = serializer.Deserialize(fs);
        if (ret != null) t = (T)ret;
        else t = default(T);
      }
    }
  }
}
