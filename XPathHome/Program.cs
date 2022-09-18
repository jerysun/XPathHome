using System.Reflection;
using System.Xml;
using XPathHome.Deserializations;
using XPathHome.Serializations;
using XPathHome.SoapSerializations;

//RemoveAttribute();
//RemoveElement();
//SerializeBook();
//SerializeLibrary();
//SerializeSoapBook();
//SerializeSoapLibrary();
DeserializeBook();
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

static void SerializeSoapBook()
{
  var book = new SoapBook()
  {
    Title = "Test",
    Year = 2016
  };
  SoapSerializer.DoSerialization<SoapBook>(book, "mysoapbook.xml");
}

static void SerializeSoapLibrary()
{
  var book = new SoapBook()
  {
    Title = "Pride and Prejudice",
    Year = 1813
  };
  var book2 = new SoapBook()
  {
    Title = "To kill a Mockingbird",
    Year = 1960
  };

  var lib = new SoapLibrary()
  {
    Books = new List<SoapBook>() { book, book2 }
  };
  SoapSerializer.DoSerialization<SoapLibrary>(lib, "mysoaplibrary.xml");
}

static void DeserializeBook()
{
  DeserializeXml.DoDeserialization(out Book? book, "book4des.xml");
  if (book == null)
  {
    Console.WriteLine("The deserialization failed!");
    return;
  }
  Console.WriteLine($"Title: {book.Title}, Author: {book.Author.Remove(0,3)}");
}