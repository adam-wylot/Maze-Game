namespace StudentEXE.View.CombatBoard;

internal class MenuGrid : RendererBase
{
    public int StartY { get; set; }
    private const int MaxOptions = 6;

    public void Update(string[] options, int selectedOption)
    {
        DrawLeft("  \x1b[36mSelect action:\x1b[0m", StartY);
        DrawLeft($"  {LightLineColor}{new string(LightLine, Width - 4)}\x1b[0m", StartY + 1);

        for (int i = 0; i < MaxOptions; i++)
        {
            if (i < options.Length)
            {
                if (i == selectedOption)
                {
                    DrawLeft($"  \x1b[47;30m > {options[i]} \x1b[0m", StartY + 3 + (i * 2));
                }
                else
                {
                    DrawLeft($"     {options[i]} ", StartY + 3 + (i * 2));
                }
            }
            else
            {
                DrawLeft("", StartY + 3 + (i * 2));
            }
        }
    }
}