using StudentEXE.Model;
using StudentEXE.View.Game;

namespace StudentEXE.InputHandlers;

internal abstract class InputHandlerBase
{
    protected readonly Dictionary<ConsoleKey, ActionChainNode> _binds = [];
    protected readonly GameModel _model;
    protected readonly MapView _view;

    public InputHandlerBase(GameModel model, MapView view)
    {
        _model = model;
        _view = view;
    }

    public void ProcessInput(ConsoleKey pressedKey)
    {
        _view.Actions.UpdateMessage("");

        if (!_binds.TryGetValue(pressedKey, out ActionChainNode? value))
        {
            Logger.Instance.Message("Pressed key is not binded to anything!");
            return;
        }

        value.Handle(_model, _view);
    }

    protected void AddBind(ConsoleKey key, List<ActionChainNode> actionsChain)
    {
        for (int i = 0; i < actionsChain.Count - 1; i++)
        {
            actionsChain[i].SetNext(actionsChain[i + 1]);
        }

        _binds.Add(key, actionsChain[0]);
        actionsChain.Clear();
    }

    public abstract void SetDefaultKeyBinds();

    public static int WaitForNumericInput()
    {
        ConsoleKeyInfo key;

        while (true)
        {
            key = Console.ReadKey(true);
            if (key.KeyChar >= '0' && key.KeyChar <= '9')
            {
                break;
            }
        }

        return key.KeyChar - '0';
    }
}
