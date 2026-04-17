using StudentEXE.Model;
using StudentEXE.View.Game;
using StudentEXE.Scenes;

namespace StudentEXE.InputHandlers;

internal class CombatInputHandler : InputHandlerBase
{
    private readonly CombatScene _scene;

    public CombatInputHandler(GameModel model, MapView view, CombatScene scene)
        : base(model, view)
    {
        _scene = scene;

        SetDefaultKeyBinds();
    }

    public override void SetDefaultKeyBinds()
    {
        AddBind(ConsoleKey.W, [new ActionChainNode(_scene.MenuUp)]);
        AddBind(ConsoleKey.UpArrow, [new ActionChainNode(_scene.MenuUp)]);

        AddBind(ConsoleKey.S, [new ActionChainNode(_scene.MenuDown)]);
        AddBind(ConsoleKey.DownArrow, [new ActionChainNode(_scene.MenuDown)]);

        AddBind(ConsoleKey.Enter, [new ActionChainNode(_scene.ExecuteAction)]);
        AddBind(ConsoleKey.E, [new ActionChainNode(_scene.ExecuteAction)]);
    }

    public new void ProcessInput(ConsoleKey key)
    {
        if (_scene.IsCombatOver)
        {
            _scene.FinishCombat();
            return;
        }

        base.ProcessInput(key);
    }
}