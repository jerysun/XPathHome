using System.Xml;

namespace RssFeedConverter
{
  public class RssFeed
  {
    public string Path { get; set; }
    private XmlDocument document;
    public RssFeed(string path)
    {
      Path = path;
      document = new XmlDocument();
      document.Load(Path);
    }

    /// <summary>
    /// Save the document
    /// </summary>
    public void Save(string fileName)
    {
      XmlDocument saveData = new XmlDocument();
      saveData.LoadXml("<rss></rss>");
      XmlNodeList? nodes = document.SelectNodes("//item");
      if (nodes == null) return;
      foreach (XmlNode node in nodes)
      {
        XmlNode importedNode = saveData.ImportNode(node, true);
        saveData.DocumentElement?.AppendChild(importedNode);
      }
      saveData.Save(fileName);
    }

    /// <summary>
    /// Get All Items from the feed
    /// </summary>
    /// <returns></returns>
    public XmlNodeList? GetItems()
    {
      return document.SelectNodes("rss/channel/item");
    }

    public XmlNodeList? GetItemsByPartialTitle(string partialTitle)
    {
      XmlNodeList? titleElements = document.SelectNodes($"//item/title[contains(.,\"{partialTitle}\")]/..");
      return titleElements;
    }

    public XmlNode? GetItemByPartialTitle(string partialTitle)
    {
      XmlNode? titleNode = document.SelectSingleNode($"//item/title[contains(.,\"{partialTitle}\")]/..");
      return titleNode;
    }

    public Uri? GetUrl(XmlNode item)
    {
      XmlNode? urlNode = item.SelectSingleNode("./link");
      return urlNode == null ? null : new Uri(urlNode.InnerText);
    }

    /// <summary>
    /// Get an item by a title
    /// </summary>
    /// <param name="title"></param>
    /// <returns></returns>
    public XmlNode? GetItemByTitle(string title)
    {
      XmlNode? titleElement = document.SelectSingleNode($"//item[title =\"{title}\"]");
      if (titleElement == null) return null;
      if (titleElement.HasChildNodes)
      {
        XmlNode? titleChild = titleElement.FirstChild;
        return titleChild;
      }
      return titleElement;
    }

    /// <summary>
    /// Get the title from a
    /// </summary>
    public string GetTitle(XmlNode item)
    {
      XmlNode? titleNode = item.SelectSingleNode("./title");
      if (titleNode == null) return string.Empty;
      return titleNode.InnerText;
    }
  }
}