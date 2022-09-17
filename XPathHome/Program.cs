using System.Xml;
using XPathHome.Serializations;

//RemoveAttribute();
//RemoveElement();
//SerializeBook();
SerializeLibrary();
Console.Read();

static XmlDocument GetDoc()
{
  XmlDocument doc = new XmlDocument();
  doc.Load("library.xml");
  return doc;
}

static void RemoveAttribute()
{
  var doc = GetDoc();
  XmlNode priceAndPrejudice = doc.SelectSingleNode("//book[title='Price and Prejudice']")!;
  if (priceAndPrejudice == null || priceAndPrejudice.Attributes == null) return;
  XmlAttribute checkedOut = priceAndPrejudice!.Attributes["checkedout"]!;
  priceAndPrejudice.Attributes.Remove(checkedOut);
  Console.WriteLine($"After removing the attribute 'checkedout', the priceAndPrejudice.Attributes.Count = {priceAndPrejudice.Attributes.Count}");
}

static void RemoveElement()
{
  var doc = GetDoc();
  XmlNode books = doc.SelectSingleNode("//books")!;
  XmlNode priceAndPrejudice = books.SelectSingleNode("//book[title='Price and Prejudice']")!;
  books.RemoveChild(priceAndPrejudice);
  var pap = books.SelectSingleNode("//book[title='Price and Prejudice']")!;
  if (pap == null)
  {
    Console.WriteLine("After removing child element, book node 'Price and Prejudice' is deleted!");
  }
  doc.Save("library-update.xml");
}

static void SerializeBook()
{
  Book book = new Book()
  {
    Title = "Gone With the Wind",
    Year = 1937,
    Author = "Margaret Mitchell",
    Color = Color.Two
  };

  SerializeXml.DoSerialization<Book>(book, "book.xml");
}

static void SerializeLibrary()
{
  var library = new Library();
  library.Books.Add(new Book()
    {
      Title = "Gone With the Wind",
      Year = 1937,
      Author = "Margaret Mitchell",
      Color = Color.Two
    });

  library.Books.Add(new Classic()
  {
    Title = "Pride and Prejudice",
    Year = 1888,
    Author = "Jane Austen",
    Color = Color.One
  });
  
  SerializeXml.DoSerialization<Library>(library, "mylibrary.xml");
}