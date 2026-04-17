using StudentEXE.Interfaces;
using StudentEXE.View;

namespace StudentEXE.Scenes;

internal class GameOverScene : IGameScene
{
    private GameOverView _view;

    public GameOverScene()
    {
        _view = new GameOverView();
    }

    public void Enter()
    {
        _view.Draw();
    }

    public void Resume()
    {
        _view.Draw();
    }

    public void HandleInput(ConsoleKey key)
    {
        Console.Clear();
        Environment.Exit(0);
    }
}