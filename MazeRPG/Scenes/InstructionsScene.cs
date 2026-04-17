using StudentEXE.Model;
using StudentEXE.View;
using StudentEXE.Interfaces;
using StudentEXE.Model.Dungeon;

namespace StudentEXE.Scenes;

internal class InstructionsScene : IGameScene
{
    private InstructionView _view;
    private Action _endCallback;

    public InstructionsScene(Action<IInitDungeonBuilder> builderFunc, Action endFunc)
    {
        _endCallback = endFunc;

        InstructionsBuilder ib = new();
        builderFunc(ib);
        _view = new InstructionView(ib.GetResult().ToArray());
    }

    public void Enter()
    {
        _view.Draw();
    }

    public void HandleInput(ConsoleKey key)
    {
        _endCallback.Invoke();
    }

    public void Resume() {}
}
