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

    public override object SolvePart2()
    {
        throw new NotImplementedException();
    }
}