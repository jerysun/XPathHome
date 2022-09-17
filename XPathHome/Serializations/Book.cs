using System.Xml.Serialization;

namespace XPathHome.Serializations
{
  [Serializable]
  public class Book
  {
    [XmlAttribute(AttributeName = "Movie")]
    public bool isMovie { get; set; } = false;
    public string Title { get; set; } = string.Empty;
    [XmlText]
    public string Author { get; set; } = string.Empty;
    [XmlIgnore]
    public int Year { get; set; }
    public Color Color { get; set; }
  }

  public enum Color
  {
    [XmlEnum(Name = "Black")]
    One,
    Two
  }
}
