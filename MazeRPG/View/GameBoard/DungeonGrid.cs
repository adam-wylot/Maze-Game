using StudentEXE.Model;
using StudentEXE.Model.Dungeon;
using StudentEXE.Model.Entities;

namespace StudentEXE.View.Game;

internal class DungeonGrid : RendererBase
{
    private const char EmptyChar = ' ';
    private const char WallChar = '█';
    private const char PlayerChar = '¶';

    private const char TopLeftCornerChar = '╔';
    private const char TopRightCornerChar = '╗';
    private const char BottomLeftCornerChar = '╚';
    private const char BottomRightCornerChar = '╝';
    private const char HorizontalWallChar = '═';
    private const char VerticalWallChar = '║';

    // Colors
    private const string ResetColor = "\x1b[0m";
    private const string EmptyCharColor = "\x1b[48;2;181;140;60m";
    private const string WallColor = "\x1b[32m";
    private const string PlayerColor = "\x1b[31m";
    private const string BorderColor = "\x1b[38;2;64;64;64m";

    public int Spacing { get; set; } = 3;

    private int _startX = 0;
    private int _startY = 0;

    public void Draw(DungeonBase dungeon, Player player)
    {
        DrawBorder(dungeon);
        DrawMaze(dungeon);
        DrawAt(PlayerColor + EmptyCharColor + PlayerChar + ResetColor, _startX + player.X, _startY + player.Y); // draw player
    }

    private void DrawMaze(DungeonBase room)
    {
        for (int y = 0; y < room.Height; y++)
        {
            for (int x = 0; x < room.Width; x++)
            {
                if (room[x, y].IsWall)
                { 
                    DrawAt(WallColor + WallChar + ResetColor, _startX + x, _startY + y);
                }
                else
                {
                    UpdateCell(room[x, y]);
                }
            }
        }
    }

    private void DrawBorder(DungeonBase room)
    {
        int width = room.Width + 2;
        int height = room.Height + 2;
        int borderX = Spacing;
        int borderY = (Height - height) / 2;

        _startX = borderX + 1;
        _startY = borderY + 1;

        // Top border
        DrawLine(' ', borderY);
        DrawLine(HorizontalWallChar, borderY, borderX, width, BorderColor + EmptyCharColor);
        DrawAt(BorderColor + EmptyCharColor + TopLeftCornerChar + "\x1b[0m", borderX, borderY);
        DrawAt(BorderColor + EmptyCharColor + TopRightCornerChar + "\x1b[0m", borderX + width - 1, borderY);

        // Bottom border
        DrawLine(' ', borderY + height - 1);
        DrawLine(HorizontalWallChar, borderY + height - 1, borderX, width, BorderColor + EmptyCharColor);
        DrawAt(BorderColor + EmptyCharColor + BottomLeftCornerChar + "\x1b[0m", borderX, borderY + height - 1);
        DrawAt(BorderColor + EmptyCharColor + BottomRightCornerChar + "\x1b[0m", borderX + width - 1, borderY + height - 1);

        // Left and right borders
        for (int y = borderY + 1; y < borderY + height - 1; y++)
        {
            DrawAt(BorderColor + EmptyCharColor + VerticalWallChar + "\x1b[0m", borderX, y);
            DrawAt(BorderColor + EmptyCharColor + VerticalWallChar + "\x1b[0m", borderX + width - 1, y);
        }
    }

    public void UpdateCell(Tile cell)
    {
        DrawAt(EmptyCharColor + (cell.GetSymbol() ?? EmptyChar) + "\x1b[0m", _startX + cell.X, _startY + cell.Y);
    }

    public void MovePlayer(Tile oldCell, Tile newCell)
    {
        UpdateCell(oldCell);
        DrawAt(PlayerColor + EmptyCharColor + PlayerChar + ResetColor, _startX + newCell.X, _startY + newCell.Y);
    }
}
