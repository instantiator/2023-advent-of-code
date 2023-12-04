namespace AdventSolutions.Solutions;

public class Day01 : AbstractDay
{
    public Day01(string inputPath) : base(inputPath)
    {
    }

    public override object SolvePart1()
    {
        var digitsOnly = lines.Select(l => l.Where(c => char.IsDigit(c)).ToArray());
        var twoDigitNumbers = digitsOnly.Select(dd => int.Parse($"{dd.First()}{dd.Last()}"));
        return twoDigitNumbers.Sum();
    }

    public override object SolvePart2()
    {
        var twoDigitNumbers = lines.Select(l => $"{FirstNumber(l)}{LastNumber(l)}").Select(int.Parse);
        return twoDigitNumbers.Sum();
    }

    private string[] numbers = new[]
    {
        "one",
        "two",
        "three",
        "four",
        "five",
        "six",
        "seven",
        "eight",
        "nine"
    };

    private int FirstNumber(string line)
    {
        for (int i = 0; i < line.Length; i++)
        {
            if (char.IsDigit(line[i]))
            {
                return int.Parse(line[i].ToString());
            }
            var remainder = line.Substring(i);
            for (int n = 0; n < numbers.Length; n++)
            {
                if (remainder.StartsWith(numbers[n]))
                {
                    return n+1; // numbers are zero indexed
                }
            }
        }
        throw new KeyNotFoundException("No number found from start of line");
    }

    private int LastNumber(string line)
    {
        for (int i = line.Length - 1; i >= 0; i--)
        {
            if (char.IsDigit(line[i]))
            {
                return int.Parse(line[i].ToString());
            }
            var remainder = line.Substring(0, i+1);
            for (int n = 0; n < numbers.Length; n++)
            {
                if (remainder.EndsWith(numbers[n]))
                {
                    return n+1; // numbers are zero indexed
                }
            }
        }
        throw new KeyNotFoundException("No number found from end of line");
    }
}