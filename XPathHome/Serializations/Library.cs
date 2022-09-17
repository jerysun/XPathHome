using System.Xml.Serialization;

namespace XPathHome.Serializations
{
  [Serializable]
  [XmlRoot(ElementName = "UniversityLibrary")]
  public class Library
  {
    [XmlArray(ElementName = "Classics")]
    public List<Book> Books { get; set; } = default!;
    public Library()
    {
      Books = new List<Book>();
    }

  }
}
