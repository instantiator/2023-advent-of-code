using System.Text.Json;

namespace AdventSolutions.Solutions;

public class Solver
{
    public static void Solve(int day, int part, string inputPath)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var solution = GetSolution(day, part, inputPath);
        Console.WriteLine(JsonSerializer.Serialize(solution, options));
    }

    private static object GetSolution(int day, int part, string inputPath)
    {
        return day switch
        {
            1 => new Day01(inputPath).Solve(part),
            2 => new Day02(inputPath).Solve(part),
            _ => throw new NotImplementedException($"Day {day} not implemented.")
        };
    }
}
