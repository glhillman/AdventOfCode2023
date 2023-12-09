// See https://aka.ms/new-console-template for more information
using System.ComponentModel.DataAnnotations;
using System.Text;

DayClass day = new DayClass();

var watch = new System.Diagnostics.Stopwatch();
watch.Start();

day.Parts1And2();

watch.Stop();
Console.WriteLine("Execution Time: {0} ms", watch.ElapsedMilliseconds);

Console.Write("Press Enter to continue...");
Console.ReadLine();

public record Part
{
    public Part(string partNum, int row, int col)
    {
        PartNum = partNum;
        Row = row;
        Col = col;
        Length = PartNum.Length;
    }
    public string PartNum { get; set; }
    public int Row { get; set; }
    public int Col { get; set; }
    public int Length { get; set; }
}

public class Gear
{
    public Gear(int partNum, int row, int col)
    {
        PartNums.Add(partNum);
        Row = row;
        Col = col;
    }
    public List<int> PartNums = new List<int>();
    public int Row { get; set; }
    public int Col { get; set; }
}
internal class DayClass
{
    char[,] _input;
    public DayClass()
    {
        LoadData();
    }

    public void Parts1And2()
    {
        List<Part> parts = new();
        for (int row = 0; row < _input.GetLength(0); row++)
        {
            for (int col = 0; col < _input.GetLength(1); col++)
            {
                if (char.IsDigit(_input[row,col]))
                {
                    // start extracting partnum
                    int colTemp = col;
                    StringBuilder sb = new();
                    while (char.IsDigit(_input[row,colTemp]))
                    {
                        sb.Append(_input[row,colTemp++]);
                        if (colTemp >= _input.GetLength(1))
                        {
                            break;
                        }
                    }
                    string pNum = sb.ToString();
                    parts.Add(new Part(pNum, row, col));
                    col += pNum.Length;
                }
            }
        }
        // find part numbers adjacent to a symbol
        int partsSum = 0;
        List<Gear> gears = new List<Gear>();
        foreach (Part part in parts)
        {
            // check all around
            bool adjacentSymbol = false;
            for (int row = part.Row - 1; !adjacentSymbol && row <= part.Row + 1; row++)
            {
                for (int col  = part.Col - 1; !adjacentSymbol && col <= part.Col + part.Length; col++)
                {
                    if (_input[row,col] != '.' && char.IsDigit(_input[row,col]) == false)
                    {
                        if (_input[row,col] == '*')
                        {
                            Gear? gear = gears.FirstOrDefault(g => g.Row == row && g.Col == col);
                            if (gear == null)
                            {
                                gears.Add(new Gear(int.Parse(part.PartNum), row, col));
                            }
                            else
                            {
                                gear.PartNums.Add(int.Parse(part.PartNum));
                            }
                        }
                        adjacentSymbol = true;
                    }
                }
            }
            if (adjacentSymbol)
            {
                partsSum += int.Parse(part.PartNum);
            }

        }
        Console.WriteLine("Part1: {0}", partsSum);
        long gearSum = 0;
        foreach (Gear gear in gears)
        {
            if (gear.PartNums.Count == 2)
            {
                gearSum += gear.PartNums[0] * gear.PartNums[1];
            }
        }
        Console.WriteLine("Part2: {0}", gearSum);
    }

    private void LoadData()
    {
        string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\input.txt";
        List<string> strings = new List<string>();
        if (File.Exists(inputFile))
        {
            string? line;
            StreamReader file = new StreamReader(inputFile);
            while ((line = file.ReadLine()) != null)
            {
                strings.Add(line);
            }

            file.Close();
        }
        _input = new char[strings.Count+2, strings[0].Length+2];
        for (int col = 0; col < _input.GetLength(0); col++)
        {
            _input[0, col] = '.';
            _input[_input.GetLength(0)-1, col] = '.';
        }
        for (int row = 1; row < _input.GetLength(0); row++)
        {
            _input[row, 0] = '.';
            _input[row, _input.GetLength(1) - 1] = '.';
        }
        for (int row = 0; row < strings.Count; row++)
        {
            for (int col = 0; col < strings[row].Length; col++)
            {
                _input[row+1,col+1] = strings[row][col];
            }
        }
    }

}
