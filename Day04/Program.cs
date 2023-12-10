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

public class Card
{
    public Card(int cardNum)
    {
        CardNum = cardNum;
        Winners = new List<int>();
        Mine = new List<int>();
    }
    public int CardNum { get; set; }
    public List<int> Winners { get; set; }
    public List<int> Mine { get; set; }
    public override string ToString()
    {
        return string.Format("Card: {0}", CardNum);
    }
}
internal class DayClass
{
    List<Card> _cards = new List<Card>();
    public DayClass()
    {
        LoadData();
    }

    public void Part1()
    {
        long pointsSum = 0;
        foreach (Card card in _cards)
        {
            long points = 0;
            foreach (int w in card.Winners)
            {
                if (card.Mine.Contains(w))
                {
                    points = points == 0 ? 1 : points + points;
                }
            }
            pointsSum += points;
        }

        Console.WriteLine("Part1: {0}", pointsSum);
    }

    public void Part2()
    {
        List<Card> copies = new List<Card>();
        for (int i = 0; i < _cards.Count; i++)
        {
            Card card = _cards[i];
            copies.Add(card);
            MakeCopies(card, copies);
        }
        copies.Sort((a, b) => (a.CardNum.CompareTo(b.CardNum)));
        Console.WriteLine("Part2: {0}", copies.Count);
    }

    private void MakeCopies(Card card, List<Card> copies)
    {
        int nWinners = NWinners(card);
        if (nWinners > 0)
        {
            int index = card.CardNum;
            for (int j = 0; j < nWinners; j++)
            {
                Card subCard = _cards[index + j];
                copies.Add(subCard);
                MakeCopies(subCard, copies);
            }
        }
    }

    private int NWinners(Card card)
    {
        List<int> winners = new List<int>();
        foreach (int w in card.Winners)
        {
            if (card.Mine.Contains(w))
            {
                winners.Add(w);
            }
        }

        return winners.Count;
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
                string[] blocks = line.Split(':', '|');
                int cardNum = int.Parse(blocks[0].Substring("Card ".Length));
                Card card = new Card(cardNum);
                string[] winners = blocks[1].Trim().Split(' ');
                foreach (string winner in winners)
                {
                    if (winner.Length > 0)
                    {
                        card.Winners.Add(int.Parse(winner));
                    }
                }
                string[] mines = blocks[2].Trim().Split(' ');
                foreach (string mine in mines)
                {
                    if (mine.Length > 0)
                    {
                        card.Mine.Add(int.Parse(mine));
                    }
                }
                _cards.Add(card);
            }

            file.Close();
        }
    }

}
