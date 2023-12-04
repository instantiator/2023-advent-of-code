namespace AdventSolutions.Solutions;

public class Day02 : AbstractDay
{
    private class Game
    {
        public int id;
        public List<Round> rounds = new List<Round>();
    }

    private class Round
    {
        public int red;
        public int green;
        public int blue;
    }

    public Day02(string inputPath) : base(inputPath)
    {
    }

    public override object SolvePart1()
    {
        var games = ReadGames(lines);

        // Determine which games would have been possible if the bag had been
        // loaded with only 12 red cubes, 13 green cubes, and 14 blue cubes.
        // What is the sum of the IDs of those games?  

        var possibleGames = games.Where(g => IsPossible(g, 12, 13, 14));
        return possibleGames.Sum(g => g.id);
    }

    public override object SolvePart2()
    {
        // For each game, find the minimum set of cubes that must have been present.
        // What is the sum of the power of these sets?
        var games = ReadGames(lines);
        var minimumRounds = games.Select(g => MinimumRound(g));
        var powers = minimumRounds.Select(r => r.red * r.green * r.blue);
        return powers.Sum();
    }

    private Round MinimumRound(Game game)
    {
        var red = game.rounds.Max(r => r.red);
        var green = game.rounds.Max(r => r.green);
        var blue = game.rounds.Max(r => r.blue);
        return new Round { red = red, green = green, blue = blue };
    }

    private bool IsPossible(Game game, int red, int green, int blue)
    {
        return game.rounds.All(r =>
            r.red <= red &&
            r.green <= green &&
            r.blue <= blue);
    }

    private IEnumerable<Game> ReadGames(IEnumerable<string> lines)
    {
        var games = new List<Game>();
        foreach (var line in lines)
        {
            var game = new Game();
            var head = line.Split(':')[0];
            var body = line.Split(':')[1];
            game.id = int.Parse(head.Split(' ')[1]);
            var roundsData = body.Split(';');
            foreach (var roundData in roundsData)
            {
                var round = new Round();
                var cubeData = roundData.Split(',').Select(c => c.Trim().Split(' '));
                foreach (var cubeInfo in cubeData)
                {
                    var number = cubeInfo[0];
                    var colour = cubeInfo[1];
                    switch (colour)
                    {
                        case "red":
                            round.red = int.Parse(number);
                            break;
                        case "green":
                            round.green = int.Parse(number);
                            break;
                        case "blue":
                            round.blue = int.Parse(number);
                            break;
                        default:
                            throw new Exception($"Unknown colour {colour}");
                    }
                }
                game.rounds.Add(round);
            }
            games.Add(game);
        }

        return games;
    }

}
