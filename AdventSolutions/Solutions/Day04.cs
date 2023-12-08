namespace AdventSolutions.Solutions;

public class Day04 : AbstractDay
{
    public Day04(string inputPath) : base(inputPath)
    {
    }

    public override object SolvePart1()
    {
        int score = 0;

        foreach (var line in lines)
        {
            var parts = line.Split('|').Select(s => s.Trim()).ToArray();

            var winners = parts[0].Split(':')[1].Trim().Split(' ').Where(s => s.Length > 0).Select(int.Parse);
            var numbers = parts[1].Trim().Split(' ').Where(s => s.Length > 0).Select(int.Parse);
            var winningNumbers = winners.Intersect(numbers);

            if (winningNumbers.Count() > 0)
            {
                score += (int)Math.Pow(2, winningNumbers.Count() - 1);
            }
        }

        return score;
    }

    public class Card
    {
        public Card(int id, IEnumerable<int> winningNumbers, IEnumerable<int> numbers)
        {
            Id = id;
            WinningNumbers = winningNumbers;
            Numbers = numbers;
        }

        public static Card From(string line)
        {
            var parts = line.Split('|').Select(s => s.Trim()).ToArray();
            var id = int.Parse(parts[0].Split(':')[0].Substring("Card ".Length).Trim());
            var winners = parts[0].Split(':')[1].Trim().Split(' ').Where(s => s.Length > 0).Select(int.Parse);
            var numbers = parts[1].Trim().Split(' ').Where(s => s.Length > 0).Select(int.Parse);
            return new Card(id, winners, numbers);
        }

        public int Id { get; set; }
        public IEnumerable<int> WinningNumbers { get; private set; }
        public IEnumerable<int> Numbers { get; private set; }
        public IEnumerable<int> Winners => WinningNumbers.Intersect(Numbers);
        public IEnumerable<int> WonIds => Enumerable.Range(Id+1, Winners.Count());
    }

    public override object SolvePart2()
    {
        var cardRepository = lines.Select(Card.From).ToList();
        var cards = lines.Select(Card.From).ToList();

        var wonCards = ExtractWonCards(0, cardRepository, cards);
        return wonCards.Count() + cards.Count();
    }

    private IEnumerable<Card> ExtractWonCards(int iteration, IEnumerable<Card> repo, IEnumerable<Card> cards)
    {
        var newCards = cards.SelectMany(c => c.WonIds).Select(id => repo.First(c => c.Id == id)).ToList();
        Console.WriteLine($"Iteration: {iteration} adds {newCards.Count()} cards");
        if (newCards.Count() > 0) {
            newCards.AddRange(ExtractWonCards(iteration+1, repo, newCards));
        }
        return newCards;
    }
}