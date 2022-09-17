using System.Xml.Serialization;

namespace XPathHome.Serializations
{
  public class Classic : Book
  {
    public bool Paperback { get; set; } = false;
  }
}
