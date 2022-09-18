using System.Xml.Linq;
using WeatherFromXml;

GetWeatherFromXml();

static void GetWeatherFromXml()
{
  // Load the weather file
  XDocument document = XDocument.Load("weather.xml");
  // create the points that will be used later
  IEnumerable<Point> locations = document.Descendants("location").Select(x => new Point()
  {
    Key = x.Element("location-key")?.Value,
    Lat = float.Parse(x.Element("point")!.Attribute("latitude")!.Value),
    Long = float.Parse(x.Element("point")!.Attribute("longitude")!.Value)
  });
  List<Point> processedPoints = new List<Point>();

  // process each point
  foreach (Point p in locations)
  {
    // filter down to the necessary nodes
    XElement parameters = document.Descendants("parameters")
    .Where(x => x.Attribute("applicablelocation")?.Value == p.Key)
    .First();

    XElement maximimums = parameters.Descendants("temperature")
    .Where(x => x.Attribute("type")?.Value == "maximum" && x.Name == "temperature")
    .First();

    XElement minimums = parameters.Descendants("temperature")
    .Where(x => x.Attribute("type")?.Value == "minimum" && x.Name == "temperature")
    .First();

    // iterate over all the elements in the maximum temperature element
    foreach (XElement max in maximimums.Descendants("value"))
    {
      p.Highs.Add(int.Parse(max.Value));
    }

    // iterate over all the elements in the minimum temperature element
    // there can be an empty value element so have to take that into account
    foreach (XElement low in minimums.Descendants("value"))
    {
      if (string.IsNullOrEmpty(low.Value)) continue;
      p.Lows.Add(int.Parse(low.Value));
    }
    processedPoints.Add(p);
  }

  // Create a root element called temps
  XElement root = new XElement("temps");
  foreach (Point p in processedPoints)
  {
    // create a base element for each key and make the lat/long children
    // of the key element
    XElement element = new XElement(
      p.Key!,
      new XElement("lat", p.Lat),
      new XElement("long", p.Long)
    );

    // create a new temperature element
    XElement temperature = new XElement("temperature");
    XElement highs = new XElement("high");
    foreach (int high in p.Highs)
    {
      XElement highElement = new XElement("value", high);
      highs.Add(highElement);
    }

    XElement lows = new XElement("lows");
    foreach (int low in p.Lows)
    {
      XElement lowElement = new XElement("value", low);
      lows.Add(lowElement);
    }
    temperature.Add(highs);
    temperature.Add(lows);
    element.Add(highs);
    element.Add(lows);
    root.Add(element);
    root.Add(temperature);
  }
  XDocument nw = new XDocument(root);
  nw.Save("temp.xml");
}