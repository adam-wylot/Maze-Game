using StudentEXE.Model;
using StudentEXE.View.Game;
using StudentEXE.InputHandlers;
using StudentEXE.Interfaces;

namespace StudentEXE.Scenes;

internal class MapScene : IGameScene
{
    private GameModel _model;
    private MapView _view;
    private MapInputHandler _inputHandler;

    public MapScene(Game game, GameModel model)
    {
        _model = model;
        _view = new MapView(_model);
        _inputHandler = new MapInputHandler(game, _model, _view);
    }

    public void Enter()
    {
        _view.Draw();
    }

    public void HandleInput(ConsoleKey key)
    {
        _inputHandler.ProcessInput(key);
    }

    public void Resume()
    {
        _view.Draw();
    }
}
