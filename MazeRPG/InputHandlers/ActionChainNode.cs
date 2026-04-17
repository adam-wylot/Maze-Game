using StudentEXE.Model;
using StudentEXE.View.Game;

namespace StudentEXE.InputHandlers;

internal class ActionChainNode
{
    private readonly Func<bool> _func;
    private readonly Action? _negativeAction;
    private ActionChainNode? _nextHandler = null;

    public ActionChainNode(Func<bool> func, Action? negativeAction = null)
    {
        _func = func;
        _negativeAction = negativeAction;
    }

    public ActionChainNode(Action func)
    {
        _func = () => WrapperMethod(func);
    }

    public void SetNext(ActionChainNode nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public void Handle(GameModel model, MapView view)
    {
        if (_func.Invoke())
        {
            _nextHandler?.Handle(model, view);
        }
        else
        {
            _negativeAction?.Invoke();
        }
    }

    private bool WrapperMethod(Action action)
    {
        action.Invoke();
        return true;
    }
}
