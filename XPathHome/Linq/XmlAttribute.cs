namespace XPathHome.Linq
{
  [AttributeUsage(AttributeTargets.Property)]
  public class XmlAttribute : Attribute
  {
    public string Name { get; set; }
    public string Value { get; set; }
    public XmlAttribute(string name, object value)
    {
      if (value != null)
      {
        Value = value.ToString();
      }
      else
      {
        Value = string.Empty;
      }
      Name = name;
    }
  }
}
