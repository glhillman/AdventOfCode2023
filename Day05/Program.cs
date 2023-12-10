// See https://aka.ms/new-console-template for more information
using System.Text;

DayClass day = new DayClass();

var watch = new System.Diagnostics.Stopwatch();
watch.Start();

day.Part1();
day.Part2();

watch.Stop();
Console.WriteLine("Execution Time: {0} ms", watch.ElapsedMilliseconds);

Console.Write("Press Enter to continue...");
Console.ReadLine();

public class RangeEntry
{
    public RangeEntry(long dstStart, long srcStart, long len)
    {
        DstStart = dstStart;
        DstEnd = dstStart + len - 1;
        SrcStart = srcStart;
        SrcEnd = srcStart + len - 1;
        Len = len;
    }
    public long SrcToDst(long src)
    {
        long value = -1;
        if (src >= SrcStart && src <= SrcEnd)
        {
            value = DstStart + (src - SrcStart);
        }
        return value;
    }
    public long DstStart { get; set; }
    public long DstEnd { get; set; }
    public long SrcStart { get; set; }
    public long SrcEnd { get; set; }
    public long Len { get; set; }
    public override string ToString()
    {
        return string.Format("dstStart: {0}, dstEnd: {1}, srcStart: {2}, srcEnd: {3}, Len: {4}",
                              DstStart, DstEnd, SrcStart, SrcEnd, Len);
    }
}

public class RangeMap
{
    public RangeMap(string name)
    {
        Name = name;
        Values = new List<RangeEntry>();
    }

    public long MapSrc(long src)
    {
        long value = -1;
        foreach (RangeEntry entry in Values)
        {
            value = entry.SrcToDst(src);
            if (value >= 0)
            {
                break;
            }
        }
        return value >= 0 ? value : src;
    }
    public string Name { get; set; }
    public List<RangeEntry> Values { get; set; }
    public override string ToString()
    {
        return string.Format("Name: {0}, NRanges: {1}", Name, Values.Count);
    }
}
internal class DayClass
{
    List<long> _seeds = new();
    List<RangeMap> _rangeMaps = new List<RangeMap>();
    public DayClass()
    {
        LoadData();
    }

    public void Part1()
    {
        long minValue = long.MaxValue;

        foreach (long seed in _seeds)
        {
            minValue = Math.Min(minValue, MapSeed(seed));
        }

        Console.WriteLine("Part1: {0}", minValue);
    }

    public void Part2()
    {
        long minValue = long.MaxValue;

        for (int i = 0; i < _seeds.Count; i += 2)
        {
            long strt = _seeds[i];
            for (long j = 0; j < _seeds[i + 1]; j++)
            {
                minValue = Math.Min(minValue, MapSeed(strt + j));
            }
        }

        Console.WriteLine("Part2: {0}", minValue);
    }

    private long MapSeed(long seed)
    {
        long nextValue = seed;
        for (int i = 0; i < _rangeMaps.Count; i++)
        {
            nextValue = _rangeMaps[i].MapSrc(nextValue);
        }
        return nextValue;
    }
    private void LoadData()
    {
        string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\input.txt";

        if (File.Exists(inputFile))
        {
            string? line;
            StreamReader file = new StreamReader(inputFile);
            line = file.ReadLine();
            string[] parts = line.Trim().Split(' ');
            for (int i = 1; i < parts.Length; i++)
            {
                _seeds.Add(long.Parse(parts[i]));
            }
            file.ReadLine();

            while ((line = file.ReadLine()) != null)
            {
                parts = line.Split(' ');
                RangeMap rangeMap = new RangeMap(parts[0]);
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Length > 0)
                    {
                        parts = line.Split(' ');
                        rangeMap.Values.Add(new RangeEntry(long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2])));
                    }
                    else
                    {
                        break;
                    }
                }
                _rangeMaps.Add(rangeMap);
            }

            file.Close();
        }
    }

}
