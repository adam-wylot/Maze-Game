namespace StudentEXE.View;

internal class GameOverView : RendererBase
{
    private static readonly string[] _gameOverArt =
    [
        " ██████   █████  ███    ███ ███████      ██████  ██    ██ ███████ ██████ ",
        "██       ██   ██ ████  ████ ██          ██    ██ ██    ██ ██      ██   ██",
        "██   ███ ███████ ██ ████ ██ █████       ██    ██ ██    ██ █████   ██████ ",
        "██    ██ ██   ██ ██  ██  ██ ██          ██    ██  ██  ██  ██      ██   ██",
        " ██████  ██   ██ ██      ██ ███████      ██████    ████   ███████ ██   ██"
    ];

    public void Draw()
    {
        Console.Clear();

        int startY = Height / 2 - _gameOverArt.Length / 2 - 2;

        for (int i = 0; i < _gameOverArt.Length; i++)
        {
            DrawCenter($"\x1b[31m{_gameOverArt[i]}\x1b[0m", startY + i);
        }

        DrawCenter("You have been defeated... Your journey ends here.", startY + _gameOverArt.Length + 2);
        DrawCenter("\x1b[90m[ Press any key to exit ]\x1b[0m", startY + _gameOverArt.Length + 4);
    }
}