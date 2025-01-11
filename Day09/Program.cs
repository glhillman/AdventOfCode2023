// See https://aka.ms/new-console-template for more information
using System.Runtime.ExceptionServices;

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
    List<int[]> _inputs = new();
    public DayClass()
    {
        LoadData();
    }

    public void Part1()
    {
        long rslt = 0;
        foreach (int[] ints in _inputs)
        {
            rslt += Reduce(ints, true);
        }

        Console.WriteLine("Part1: {0}", rslt);
    }



    public void Part2()
    {
        long rslt = 0;
        foreach (int[] ints in _inputs)
        {
            rslt += Reduce(ints, false);
        }

        Console.WriteLine("Part2: {0}", rslt);
    }

    private int Reduce(int[] ints, bool atEnd)
    {
        int[] nextInts = new int[ints.Length - 1];
        bool allZero = true;

        for (int i = 0; i < ints.Length-1; i++)
        {
            int diff = ints[i+1] - ints[i];
            if (diff != 0)
            {
                allZero = false;
            }
            nextInts[i] = diff;
        }
        if (atEnd)
        {
            if (allZero)
            {
                return ints[ints.Length - 1];
            }
            else
            {
                return ints[ints.Length - 1] + Reduce(nextInts, atEnd);
            }
        }
        else
        {
            if (allZero)
            {
                return ints[0];
            }
            else
            {
                return ints[0] - Reduce(nextInts, atEnd);
            }

        }
    }
    private void LoadData()
    {
        string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\input.txt";

        if (File.Exists(inputFile))
        {
            string? line;
            StreamReader file = new StreamReader(inputFile);
            while ((line = file.ReadLine()) != null)
            {
                string[] split = line.Split(' ');
                int[] ints = new int[split.Length];
                for (int i = 0; i < ints.Length; i++)
                {
                    ints[i] = int.Parse(split[i]);
                }
                _inputs.Add(ints);
            }

            file.Close();
        }
    }

}
