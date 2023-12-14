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

internal class DayClass
{
    List<long> _time = new();
    List<long> _dist = new();

    public DayClass()
    {
        LoadData();
    }

    public void Part1()
    {
        long nBetter;
        long betterProduct = 1;

        for (int i = 0; i < _time.Count; i++)
        {
            nBetter = CalcNBetter(_time[i], _dist[i]);
            betterProduct *= nBetter;
        }

        Console.WriteLine("Part1: {0}", betterProduct);
    }

    public void Part2()
    {
        StringBuilder sb = new StringBuilder();
        foreach (int t in _time)
        {
            sb.Append(t);
        }
        long lTime = long.Parse(sb.ToString());
        sb.Clear();
        foreach (int d in _dist)
        {
            sb.Append(d);
        }
        long lDist = long.Parse(sb.ToString());

        long nBetter = CalcNBetter(lTime, lDist);
   
        Console.WriteLine("Part2: {0}", nBetter);
    }

    private long CalcNBetter(long time, long targetDist)
    {
        long nBetter = 0;

        
        long button = 0;
        long remaingTime = time;
        while (button < time)
        {
            long travelled = button * (time - button);
            nBetter += travelled > targetDist ? 1 : 0;
            button++;
        }
        return nBetter;
    }
    private void LoadData()
    {
        string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\input.txt";

        if (File.Exists(inputFile))
        {
            string? line;
            StreamReader file = new StreamReader(inputFile);
            line = file.ReadLine().Replace("  ", " ");
            string[] parts = line.Split(':', ' ');
            foreach (string part in parts)
            {
                int num;
                if (int.TryParse(part, out num))
                {
                    _time.Add(num);
                }
            }
            line = file.ReadLine();
            parts = line.Split(':', ' ');
            foreach (string part in parts)
            {
                int num;
                if (int.TryParse(part, out num))
                {
                    _dist.Add(num);
                }
            }

            file.Close();
        }
    }

}
