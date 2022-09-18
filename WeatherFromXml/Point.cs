namespace WeatherFromXml
{
  public class Point
  {
    public float? Lat { get; set; }
    public float? Long { get; set; }
    public string? Key { get; set; }
    public Point()
    {
      Highs = new List<int>();
      Lows = new List<int>();
    }
    public List<int> Highs { get; set; }
    public List<int> Lows { get; set; }
  }
}
