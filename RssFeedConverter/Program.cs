using RssFeedConverter;
using System.Xml.Xsl;

RssToHtml();

static void RssToHtml()
{
  //RssFeed feed = new RssFeed("https://www.usa.gov/rss/updates.xml"); // The exported links don't work due to the removal from government
  RssFeed feed = new RssFeed("https://rss.nytimes.com/services/xml/rss/nyt/World.xml");
  XslCompiledTransform xslt = new XslCompiledTransform();
  feed.Save("test.xml");
  if (!File.Exists("test.xml")) return;
  xslt.Load("RssFeedLink.xslt");
  xslt.Transform("test.xml", "output.html");
}
