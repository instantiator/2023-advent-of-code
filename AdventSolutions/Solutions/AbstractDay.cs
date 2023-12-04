namespace AdventSolutions.Solutions;

public abstract class AbstractDay
{
    protected readonly string[] lines;

    protected AbstractDay(string inputPath)
    {
        lines = File.ReadAllLines(inputPath);
    }

    public object Solve(int part)
    {
        return part switch
        {
            1 => SolvePart1(),
            2 => SolvePart2(),
            _ => throw new IndexOutOfRangeException("Part must be 1 or 2")
        };
    }

    public abstract object SolvePart1();
    public abstract object SolvePart2();
}