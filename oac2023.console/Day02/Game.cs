namespace aoc2023.Day02;

public class Game
{
    public int Id { get; set; }
    public List<GameSet> Sets { get; } = new List<GameSet>();

    public Game(string text)
    {
        var slices = text.Split(':');
        ParseGame(slices[0]);
        ParseSets(slices[1]);
    }

    private void ParseGame(string text)
    {
        Id = Convert.ToInt32(text.Split(" ")[1]);
    }

    private void ParseSets(string text)
    {
        var slices = text.Split(";");

        foreach (var slice in slices)
            Sets.Add(new GameSet(slice));
    }
}