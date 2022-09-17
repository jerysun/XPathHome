using System.Xml.Serialization;

namespace XPathHome.SoapSerializations
{
  [SoapType("Book", "http://www.apress.com")]
  public class SoapBook
  {
    [SoapElement("Title")]
    public string Title { get; set; } = string.Empty;
    [SoapIgnore]
    public int Year { get; set; } = 0;
  }
}
