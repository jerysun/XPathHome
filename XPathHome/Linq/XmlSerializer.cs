using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XPathHome.Linq
{
  public class XmlSerializer<T>
  {
    private Type type;
    private string defaultNamespace;

    public XmlSerializer(string defaultNamespace)
    {
      type = typeof(T);
      this.defaultNamespace = defaultNamespace;
    }

    private XElement CreateElement(PropertyInfo propertyInfo, T serializableObject)
    {
      XElement element = new XElement(propertyInfo.Name);
      XmlAttribute xmlAttribute = propertyInfo.GetCustomAttribute(typeof(XmlAttribute)) as XmlAttribute;
      if (xmlAttribute != null)
      {
        element.Add(new XAttribute(xmlAttribute.Name, xmlAttribute.Value));
      }

      // create a child element using the property name and get the property value off of the passed in object
      element.Add(new XElement(propertyInfo.Name, type.GetProperty(propertyInfo.Name)
        .GetValue(serializableObject, null).ToString()));
      return element;
    }

    public void Serialize(Stream stream, T serializableObject)
    {
      // get the type of the passed in class
      Type type = typeof(T);
      // Check to see if the class has the Serializable attribute
      if (type.CustomAttributes.Any(x => x.AttributeType == typeof(SerializableAttribute)))
      {
        XElement element = new XElement(type.Name);
        // This class has the ability to be serialized since the serializable attribute was on the class
        PropertyInfo[] properties = type.GetProperties();
        // filter out properties that have the ignore on them
        List<PropertyInfo> serializableProperties = properties
          .Where(x => x.GetCustomAttribute(typeof(IgnoreAttribute)) == null)
          .ToList();
        
        foreach (PropertyInfo pi in serializableProperties)
        {
          element.Add(CreateElement(pi, serializableObject));
        }
        byte[] bytes = Encoding.UTF8.GetBytes(element.ToString());
        stream.Write(bytes, 0, bytes.Length);
      }
     }

    public T Deserialize(string filePath)
    {
      T loaded = Activator.CreateInstance<T>();
      XDocument doc = XDocument.Load(filePath);
      XElement root = doc.Root;

      if (root.Name == typeof(T).Name)
      {
        foreach (XElement child in root.Elements())
        {
          /// Check to see if this is a class or just a property
          loaded.GetType().GetProperty(child.Name.ToString()).
          SetValue(loaded, child.Value);
        }
      }
      else
      {
        throw new ArgumentException("The type passed in does not match the XML");
      }
      return loaded;
    }
  }
}
