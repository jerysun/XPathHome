using System.Xml.Serialization;

namespace XPathHome.SoapSerializations
{
  [SoapType("Library", "http://www.apress.com/Library")]
  public class SoapLibrary
  {
    public List<SoapBook> Books { get; set; } = default!;
  }
}
