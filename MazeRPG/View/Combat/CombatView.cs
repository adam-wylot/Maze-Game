using StudentEXE.View.CombatBoard;

namespace StudentEXE.View.Combat;

internal class CombatView : RendererBase
{
    public EnemyGrid EnemyGrid { get; private set; } = new();
    public PlayerGrid PlayerGrid { get; private set; } = new();
    public MenuGrid MenuGrid { get; private set; } = new();
    public LogGrid LogGrid { get; private set; } = new();

    public void InitializeLayout()
    {
        Console.Clear();

        int middleY = Height / 2 + 2;
        int col1End = Width / 3;
        int col2End = (Width / 3) * 2;

        // borders
        DrawLine(HeavyLine, 0);
        DrawLine(HeavyLine, middleY);
        DrawLine(HeavyLine, Height - 1);

        for (int y = middleY + 1; y < Height - 1; y++)
        {
            DrawAt('║', col1End, y);
            DrawAt('║', col2End, y);
        }

        // grids configuration
        EnemyGrid.X = 0;
        EnemyGrid.Width = Width;
        EnemyGrid.StartY = 2;

        PlayerGrid.X = 0;
        PlayerGrid.Width = col1End - 1;
        PlayerGrid.StartY = middleY + 2;

        MenuGrid.X = col1End + 1;
        MenuGrid.Width = col2End - col1End - 1;
        MenuGrid.StartY = middleY + 2;

        LogGrid.X = col2End + 1;
        LogGrid.Width = Width - col2End - 2;
        LogGrid.StartY = middleY + 2;
    }
}