// See https://aka.ms/new-console-template for more information
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
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
    List<string> _input = new List<string>();
    public DayClass()
    {
        LoadData();
    }

    public void Part1()
    {
        List<char> nums = new List<char>();
        int sum = 0;
        foreach (string input in _input)
        {
            nums.Clear();
            foreach (char c in input)
            {
                if (char.IsDigit(c))
                {
                    nums.Add(c);
                }
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(nums[0]);
            sb.Append(nums[nums.Count - 1]);
            int num = int.Parse(sb.ToString());
            //Console.WriteLine(num);
            sum += num;
            
        }

        Console.WriteLine("Part1: {0}", sum);
    }

    public void Part2()
    {
        Dictionary<string, int> pairs = new();
        pairs["1"] = 1;
        pairs["2"] = 2;
        pairs["3"] = 3;
        pairs["4"] = 4;
        pairs["5"] = 5;
        pairs["6"] = 6;
        pairs["7"] = 7;
        pairs["8"] = 8;
        pairs["9"] = 9;
        pairs["one"] = 1;
        pairs["two"] = 2;
        pairs["three"] = 3;
        pairs["four"] = 4;
        pairs["five"] = 5;
        pairs["six"] = 6;
        pairs["seven"] = 7;
        pairs["eight"] = 8;
        pairs["nine"] = 9;
        int sum = 0;
        int value = 0;

        List<string> sNums = pairs.Keys.ToList();

        foreach (string input in _input)
        {
            value = 0;
            int index = 0;
            string sub;
            while (value == 0)
            {
                sub = input.Substring(index++);
                foreach (string sNum in sNums)
                {
                    if (sub.StartsWith(sNum))
                    {
                        value = pairs[sNum] * 10;
                        break;
                    }
                }
            }
            index = input.Length - 1;
            while (index >= 0)
            {
                sub = input.Substring(index--);
                foreach (string sNum in sNums)
                {
                    if (sub.StartsWith(sNum))
                    {
                        value += pairs[sNum];
                        index = -1;
                        break;
                    }
                }
            }
            sum += value;
        }

        Console.WriteLine("Part2: {0}", sum);
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
