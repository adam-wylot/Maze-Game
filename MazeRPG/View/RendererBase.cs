using System.Text.RegularExpressions;

namespace StudentEXE.View;

internal abstract class RendererBase
{
    protected const char HeavyLine = '═';
    protected const char LightLine = '╌';

    protected const string HeavyLineColor = "\x1b[33m";
    protected const string LightLineColor = "\x1b[93m";

    public int X { get; set; } = 0;
    public int Width { get; set; } = 0;
    public int Height { get; set; } = 0;

    public RendererBase()
    {
        Width = Console.WindowWidth;
        Height = Console.WindowHeight;
    }

    protected void ClearLine(int row)
    {
        DrawLeft("", row);
    }

    protected void DrawLeft(string str, int row)
    {
        (int oldX, int oldY) = Console.GetCursorPosition();
        Console.SetCursorPosition(X, row);

        int freeSpace = CalculateLeftSpace(str);

        if (freeSpace < 0)
        {
            Console.Write(str.Substring(0, Width));
        }
        else
        {
            Console.Write(str + new string(' ', freeSpace) + "\x1b[0m"); // printing spaces to clear the line
        }

        Console.SetCursorPosition(oldX, oldY);
    }

    protected void DrawCenter(string str, int row)
    {
        (int oldX, int oldY) = Console.GetCursorPosition();
        Console.SetCursorPosition(X, row);

        int freeSpace = CalculateLeftSpace(str);

        if (freeSpace < 0)
        {
            Console.Write(str.Substring(0, Width));
        }
        else
        {
            int leftSpaces = freeSpace / 2;
            int rightSpaces = freeSpace - leftSpaces; // printing spaces to clear the line
            Console.Write(new string(' ', leftSpaces) + str + new string(' ', rightSpaces) + "\x1b[0m");
        }

        Console.SetCursorPosition(oldX, oldY);
    }

    protected void DrawRight(string str, int row)
    {
        (int oldX, int oldY) = Console.GetCursorPosition();
        Console.SetCursorPosition(X, row);

        int freeSpace = CalculateLeftSpace(str);

        if (freeSpace < 0)
        {
            Console.Write(str.Substring(0, Width));
        }
        else
        {
            Console.Write(new string(' ', freeSpace) + str + "\x1b[0m");
        }

        Console.SetCursorPosition(oldX, oldY);
    }

    protected void DrawLine(char c, int row, int startX = 0, int width = -1, string colorCode = "\x1b[0m")
    {
        if (width > Width) width = Width;
        if (width == -1) width = Width;

        if (colorCode.Equals("\x1b[0m"))
        {
            if (c == HeavyLine)
            {
                colorCode = HeavyLineColor;
            }
            else if (c == LightLine)
            {
                colorCode = LightLineColor;
            }
        }

        (int oldX, int oldY) = Console.GetCursorPosition();

        Console.SetCursorPosition(X + startX, row);
        Console.Write(colorCode + new string(c, width) + "\x1b[0m");

        Console.SetCursorPosition(oldX, oldY);
    }

    protected void DrawAt(char c, int x, int y)
    {
        (int oldX, int oldY) = Console.GetCursorPosition();
        Console.SetCursorPosition(X + x, y);

        Console.Write(c);
        
        Console.SetCursorPosition(oldX, oldY);
    }

    protected void DrawAt(string str, int x, int y)
    {
        (int oldX, int oldY) = Console.GetCursorPosition();
        Console.SetCursorPosition(X + x, y);

        Console.Write(str);

        Console.SetCursorPosition(oldX, oldY);
    }

    private int CalculateLeftSpace(string str)
    {
        int counter = Width - str.Length;

        var matches = Regex.Matches(str, @"\x1B\[[0-9;]*m");

        foreach (Match match in matches)
        {
            counter += match.Value.Length;
        }
    
        return counter;
    }
}
