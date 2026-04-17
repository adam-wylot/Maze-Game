namespace StudentEXE.View.CombatBoard;

internal class LogGrid : RendererBase
{
    public int StartY { get; set; }
    private const int MaxLogs = 8;

    public void Update(string[] log)
    {
        DrawLeft("  \x1b[33mCombat log:\x1b[0m", StartY);
        DrawLeft($"  {LightLineColor}{new string(LightLine, Width - 4)}\x1b[0m", StartY + 1);

        for (int i = 0; i < MaxLogs; i++)
        {
            if (i < log.Length)
            {
                string color = i == 0 ? "\x1b[97m" : "\x1b[90m";
                DrawLeft($"  {color}{log[i]}\x1b[0m", StartY + 3 + (i * 2));
            }
            else
            {
                DrawLeft("", StartY + 3 + (i * 2));
            }
        }
    }
}