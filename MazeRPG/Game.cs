using StudentEXE.Model;
using StudentEXE.Model.Dungeon;
using StudentEXE.Interfaces;
using StudentEXE.Scenes;
using System.Text;

namespace StudentEXE;

internal class Game
{
    private const int requiredWidth = 90;
    private const int requiredHeight = 50;

    private Stack<IGameScene> _scenes = new();

    public void PushScene(IGameScene scene)
    {
        _scenes.Push(scene);
        scene.Enter();
    }

    public void PopScene()
    {
        if (_scenes.Count <= 0)
        {
            return;
        }

        _scenes.Pop();
        _scenes.Peek().Resume();
    }


    public void Run()
    {
        Initialize();

        var dir = new DungeonDirector();
        Action<IInitDungeonBuilder> dirFunc = dir.BuildFullOption;

        var gameModel = new GameModel(dirFunc);

        var gameOverScene = new GameOverScene();
        _scenes.Push(gameOverScene);

        var mapScene = new MapScene(this, gameModel);
        _scenes.Push(mapScene);

        var instrScene = new InstructionsScene(dirFunc, PopScene);
        PushScene(instrScene);

        MainLoop();
    }

    private void Initialize()
    {
        int width;
        int height;

        while (true)
        {
            Console.Clear();
            Console.Title = "Student.exe";
            Console.CursorVisible = false;

            Console.WriteLine("Set Fullscreen for better experience (F11 or Alt+Enter).");
            Console.WriteLine("[Press any key to continue...]");
            Console.ReadKey(true);

            Console.Clear();
            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.UTF8;

            width = Console.WindowWidth;
            height = Console.WindowHeight;

            if (width >= requiredWidth && height >= requiredHeight)
            {
                break;
            }

            HaveToZoomOut();
        }
    }

    private void HaveToZoomOut()
    {
        Console.Clear();
        Console.WriteLine("Not enought space in the window.");
        Console.WriteLine("You have to \"zoom out\" console (Ctrl + Scroll down).");
        Console.WriteLine("[Press any key to continue...]");
        Console.ReadKey(true);
    }

    private void MainLoop()
    {
        while (true)
        {
            if (_scenes.Count == 0) return; // end of a game

            var key = Console.ReadKey(true).Key;
            var activeScene = _scenes.Peek();
            activeScene.HandleInput(key);
        }
    }
}
