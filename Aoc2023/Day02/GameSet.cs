namespace Aoc2023.Day02;

public class GameSet
{
    public int R { get; set; }
    public int G { get; set; }
    public int B { get; set; }

    public GameSet(string text)
    {
        var map = text
            .Split(",")
            .Select(s => s.Trim().Split(" "));

        R = Convert.ToInt32(map.Where(e => e[1] == "red").Select(e => e[0]).FirstOrDefault() ?? "0");
        G = Convert.ToInt32(map.Where(e => e[1] == "green").Select(e => e[0]).FirstOrDefault() ?? "0");
        B = Convert.ToInt32(map.Where(e => e[1] == "blue").Select(e => e[0]).FirstOrDefault() ?? "0");
    }
}