using System.Drawing;

namespace AdventSolutions.Solutions;

public class Day03 : AbstractDay
{
    public Day03(string inputPath) : base(inputPath)
    {
    }

    private class PartNumberCandidate
    {
        public int row;
        public int index;
        public string word = null!;
        public int len => word.Length;
        public Point[] corners =>
            new Point[] {
                new Point(index-1, row-1),
                new Point(index+len, row+1),
            };

        public IEnumerable<Point> footprint(int maxRow, int maxCol)
        {
            return footprint(corners, maxRow, maxCol);
        }

        public static IEnumerable<Point> footprint(Point[] corners, int maxRow, int maxCol)
        {
            return
                Enumerable.Range(corners[0].X, corners[1].X - corners[0].X + 1)
                .SelectMany(x => Enumerable.Range(corners[0].Y, corners[1].Y - corners[0].Y + 1).Select(y => new Point(x, y)))
                .Where(p => p.X >= 0 && p.Y >= 0 && p.X <= maxCol && p.Y <= maxRow);
        }
    }

    private IEnumerable<PartNumberCandidate> GetAllParts()
    {
        var parts = new List<PartNumberCandidate>();
        var maxCol = lines.Max(l => l.Length) - 1;
        var maxRow = lines.Length - 1;
        foreach (var lineData in lines.Select((line, row) => new { line, row }))
        {
            // find the starting indices of each number
            var indices = lineData.line
                .Select((c, i) => new { c, i })
                .Where(startChar => char.IsDigit(startChar.c) && (startChar.i == 0 || !char.IsDigit(lineData.line[startChar.i - 1])))
                .Select(startChar => startChar.i);

            var candidates = indices
                .Select(index => new PartNumberCandidate
                {
                    row = lineData.row,
                    index = index,
                    word = string.Concat(lineData.line.Substring(index).TakeWhile(char.IsDigit)),
                });

            parts.AddRange(candidates
                .Where(c => c
                    .footprint(maxRow, maxCol)
                    .Any(p => lines[p.Y][p.X] != '.' && !char.IsDigit(lines[p.Y][p.X]))));
        }
        return parts;
    }

    public override object SolvePart1()
    {
        var parts = GetAllParts();
        return parts.Sum(c => int.Parse(c.word));
    }

    public override object SolvePart2()
    {
        var parts = GetAllParts();

        var maxCol = lines.Max(l => l.Length) - 1;
        var maxRow = lines.Length - 1;
        var gearRatios = new List<int>();

        foreach (var lineData in lines.Select((line, row) => new { line, row }))
        {
            var candidates = lineData.line
                .Select((c, i) => new { c, i })
                .Where(startChar => startChar.c == '*')
                .Select(startChar => new PartNumberCandidate()
                {
                    row = lineData.row,
                    index = startChar.i,
                    word = "*"
                });

            foreach (var candidate in candidates)
            {
                var connectedParts = new List<PartNumberCandidate>();
                foreach (var part in parts)
                {
                    if (part.footprint(maxRow, maxCol).Any(p => p.Equals(new Point(candidate.index, candidate.row))))
                    {
                        connectedParts.Add(part);
                    }
                }
                if (connectedParts.Count == 2)
                {
                    gearRatios.Add(int.Parse(connectedParts[0].word) * int.Parse(connectedParts[1].word));
                }
            }
        }

        return gearRatios.Sum();
    }


}