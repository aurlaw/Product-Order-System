using System.Text;

//OrderNumGenerator.Generate("W", id:5)
namespace AurSystem.Framework;

  public class OrderNumGenerator
  {
    private static object obj = new object();
    private static int GuidInt { get { return Guid.NewGuid().GetHashCode(); } }
    private static string GuidIntStr { get { return Math.Abs(GuidInt).ToString(); } }

    public static string Generate(string mark, int timeType = 4, int id = 0)
    {
      lock (obj)
      {
//        var number = mark;
        var sb = new StringBuilder();
        var ticks = (DateTime.Now.Ticks - GuidInt).ToString();
        var fillCount = 0; // fill digits
        
        sb.Append(GetTimeStr(timeType, out fillCount));
        if (id > 0)
        {
          sb.Append( ticks.Substring(ticks.Length - (fillCount + 3)) + id.ToString().PadLeft(1, '0'));
        }
        else
        {
          sb.Append(ticks.Substring(ticks.Length - (fillCount + 3)) + GuidIntStr.PadLeft(10, '0'));
        }
        var v = Hyphenate(sb.ToString(),6);
        return $"{mark}{v}";
      }
    }
    
    private static string GetTimeStr(int timeType, out int fillCount)
    {
      var time = DateTime.Now;
      if (timeType == 1)
      {
        fillCount = 6;
        return time.ToString("yyyyMMdd");
      }
      else if (timeType == 2)
      {
        fillCount = 4;
        return time.ToString("yyyyMMddHH");
      }
      else if (timeType == 3)
      {
        fillCount = 2;
        return time.ToString("yyyyMMddHHmm");
      }
      else
      {
        fillCount = 0;
        return time.ToString("yyyyMMddHHmmss");
      }
    }
    
    private static string Hyphenate(string str, int pos) {
      return string.Join("-",
        str.Select((c, i) => new { c, i })
          .GroupBy(x => x.i / pos)
          .Select(g => string.Join("", g.Select(x => x.c))));
    }
  }