// See https://aka.ms/new-console-template for more information
using Day08;
using System;

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
    char[] _steps;
    Dictionary<string, (string, string)> _maps = new();
    public DayClass()
    {
        LoadData();
    }

    public void Part1()
    {
        int index = 0;
        int wrap = _steps.Length;
        int count = 0;
        string key = "AAA";
        while (key != "ZZZ")
        {
            (string left, string right) = _maps[key];
            key = _steps[index] == 'L' ? left : right;
            index = (index + 1) % wrap;
            count++;
        }
        Console.WriteLine("Part1: {0}", count);
    }

    public void Part2()
    {
        List<string> set = new List<string>();
        int index = 0;
        long count = 0;
        int wrap = _steps.Length;

        foreach (string key in _maps.Keys)
        {
            if (key.EndsWith('A'))
            {
                set.Add(key);
            }
        }
        List<long> zList = new();
        foreach (string key in set)
        {
            string aKey = key;
            index = 0;
            count = 0;
            while (aKey.EndsWith('Z') == false)
            {
                (string left, string right) = _maps[aKey];
                aKey = _steps[index] == 'L' ? left : right;
                index = (index + 1) % wrap;
                count++;
            }
            zList.Add(count);
        }
        long lcm = LCM_GCD.LCMOfArray(zList.ToArray());
        Console.WriteLine("Part2: {0}", lcm);
    }

    private void LoadData()
    {
        string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\input.txt";

        if (File.Exists(inputFile))
        {
            string? line;
            StreamReader file = new StreamReader(inputFile);
            line = file.ReadLine();
            _steps = line.ToArray();
            file.ReadLine();
            while ((line = file.ReadLine()) != null)
            {
                string[] parts = line.Split(' ', '=', '(', ')', ',');
                _maps[parts[0]] = (parts[4], parts[6]);
            }

            file.Close();
        }
    }

}
