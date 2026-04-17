using StudentEXE.Model.Entities;

namespace StudentEXE.View.CombatBoard;

internal class PlayerGrid : RendererBase
{
    public int StartY { get; set; }

    public void Update(Player player)
    {
        DrawLeft($"  \x1b[32m{player.Name}\x1b[0m", StartY);
        DrawLeft($"  {LightLineColor}{new string(LightLine, Width - 4)}\x1b[0m", StartY + 1);

        string hpBar = GenerateHealthBar(player.Health, 100, 15);
        DrawLeft($"  HP:  {hpBar} \x1b[32m{player.Health}\x1b[0m", StartY + 3);

        DrawLeft($"  STR: \x1b[93m{player.Strength}\x1b[0m  |  AGI: \x1b[93m{player.Agility}\x1b[0m", StartY + 5);
        DrawLeft($"  LUK: \x1b[93m{player.Luck}\x1b[0m  |  WIS: \x1b[93m{player.Wisdom}\x1b[0m", StartY + 6);
    }

    private string GenerateHealthBar(int current, int max, int length)
    {
        if (max <= 0) return new string('░', length);
        int filledLength = (int)Math.Round((double)current / max * length);
        filledLength = Math.Max(0, Math.Min(length, filledLength));
        string filled = new string('█', filledLength);
        string empty = new string('░', length - filledLength);

        double percentage = (double)current / max;
        string color = "\x1b[32m";
        if (percentage < 0.5) color = "\x1b[33m";
        if (percentage < 0.25) color = "\x1b[31m";

        return $"{color}{filled}\x1b[90m{empty}\x1b[0m";
    }
}