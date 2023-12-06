// See https://aka.ms/new-console-template for more information
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
    List<string> _input = new();
    public DayClass()
    {
        LoadData();
    }

    public void Part1()
    {
        int idSum = 0;

        foreach (string input in _input)
        {
            bool possible = true;
            string[] groups = input.Split(':', ';');
            string[] idSplit = groups[0].Split(' ');
            int id = int.Parse(idSplit[1]);
            for (int i = 1; possible && i < groups.Length; i++)
            {
                string[] colorNums = groups[i].Trim().Split(' ');
                int j = 0;
                while (j < colorNums.Length && possible)
                {
                    int num = int.Parse(colorNums[j]);
                    string color = colorNums[j + 1].Replace(",", "");
                    switch (color)
                    {
                        case "red":
                            if (num > 12)
                            {
                                possible = false;
                            }
                            break;
                        case "green":
                            if (num > 13)
                            {
                                possible = false;
                            }
                            break;
                        case "blue":
                            if (num > 14)
                            {
                                possible = false;
                            }
                            break;
                    }
                    j += 2;
                }
            }
            if (possible)
            {
                idSum += id;
            }
        }

        Console.WriteLine("Part1: {0}", idSum);
    }

    public void Part2()
    {
        int powerSum = 0;

        foreach (string input in _input)
        {
            int redMax = int.MinValue;
            int greenMax = int.MinValue;
            int blueMax = int.MinValue;

            string[] groups = input.Split(':', ';');
            string[] idSplit = groups[0].Split(' ');
            int id = int.Parse(idSplit[1]);
            for (int i = 1; i < groups.Length; i++)
            {
                string[] colorNums = groups[i].Trim().Split(' ');
                int j = 0;
                while (j < colorNums.Length)
                {
                    int num = int.Parse(colorNums[j]);
                    string color = colorNums[j + 1].Replace(",", "");
                    switch (color)
                    {
                        case "red":
                            redMax = Math.Max(redMax, num);
                            break;
                        case "green":
                            greenMax = Math.Max(greenMax, num);
                            break;
                        case "blue":
                            blueMax = Math.Max(blueMax, num);
                            break;
                    }
                    j += 2;
                }
            }
            redMax = Math.Max(redMax, 1);
            greenMax = Math.Max(greenMax, 1);
            blueMax = Math.Max(blueMax, 1);
            int power = redMax * greenMax * blueMax;
            powerSum += power;
        }

        Console.WriteLine("Part2: {0}", powerSum);
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
                _input.Add(line);
            }

            file.Close();
        }
    }

}
