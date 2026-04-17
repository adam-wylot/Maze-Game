using StudentEXE.Model;
using StudentEXE.View.Game;

namespace StudentEXE.InputHandlers;

internal class MapInputHandler : InputHandlerBase
{
    private Game _game;

    public MapInputHandler(Game game, GameModel model, MapView view)
        : base(model, view)
    {
        _game = game;

        SetDefaultKeyBinds();
    }

    public override void SetDefaultKeyBinds()
    {
        List<ActionChainNode> c = [];

        // ===== Player movement =====
        // Moving up
        c.Add(new ActionChainNode(_model.MovePlayerUp, () => Logger.Instance.Message("You can't move up.")));
        c.Add(new ActionChainNode(() => PlayerActions.DoMove(_model, _view, dy: -1)));
        c.Add(new ActionChainNode(() => PlayerActions.CheckNoEnemy(_model), () => PlayerActions.PerformCombat(_game, _model, _view)));
        c.Add(new ActionChainNode(_view.Actions.UpdateActions));
        AddBind(ConsoleKey.W, c);

        // Moving down
        c.Add(new ActionChainNode(_model.MovePlayerDown, () => Logger.Instance.Message("You can't move down.")));
        c.Add(new ActionChainNode(() => PlayerActions.DoMove(_model, _view, dy: 1)));
        c.Add(new ActionChainNode(() => PlayerActions.CheckNoEnemy(_model), () => PlayerActions.PerformCombat(_game, _model, _view)));
        c.Add(new ActionChainNode(_view.Actions.UpdateActions));
        AddBind(ConsoleKey.S, c);

        // Moving left
        c.Add(new ActionChainNode(_model.MovePlayerLeft, () => Logger.Instance.Message("You can't move left.")));
        c.Add(new ActionChainNode(() => PlayerActions.DoMove(_model, _view, dx: -1)));
        c.Add(new ActionChainNode(() => PlayerActions.CheckNoEnemy(_model), () => PlayerActions.PerformCombat(_game, _model, _view)));
        c.Add(new ActionChainNode(_view.Actions.UpdateActions));
        AddBind(ConsoleKey.A, c);

        // Moving Right
        c.Add(new ActionChainNode(_model.MovePlayerRight, () => Logger.Instance.Message("You can't move right.")));
        c.Add(new ActionChainNode(() => PlayerActions.DoMove(_model, _view, dx: 1)));
        c.Add(new ActionChainNode(() => PlayerActions.CheckNoEnemy(_model), () => PlayerActions.PerformCombat(_game, _model, _view)));
        c.Add(new ActionChainNode(_view.Actions.UpdateActions));
        AddBind(ConsoleKey.D, c);

        // ===== Player's actions =====
        // dropping
        c.Add(new ActionChainNode(() => _model.Player.Inventory.Items.Count > 0, () => Logger.Instance.Message("You don't have any items.")));
        c.Add(new ActionChainNode(() => PlayerActions.DoDrop(_model, _view)));
        c.Add(new ActionChainNode(_view.Actions.UpdateActions));
        AddBind(ConsoleKey.Q, c);

        // Pickuping
        c.Add(new ActionChainNode(() =>
        {
            int pX = _model.Player.X;
            int pY = _model.Player.Y;
            var tile = _model.Dungeon[pX, pY];

            return tile.Items.Count > 0;
        },
            () => Logger.Instance.Message("There is no item on the tile!")
        ));
        c.Add(new ActionChainNode(() =>
        {
            int pX = _model.Player.X;
            int pY = _model.Player.Y;
            var tile = _model.Dungeon[pX, pY];
            var enemy = tile.TileEnemy;

            return enemy == null;
        },
            () => Logger.Instance.Message("Can't pick up items while enemy is at the tile!")
        ));
        c.Add(new ActionChainNode(_model.Player.Inventory.CanAddItem, () => Logger.Instance.Message("Your inventory is full!")));
        c.Add(new ActionChainNode(() => PlayerActions.DoPickup(_model, _view)));
        c.Add(new ActionChainNode(_view.Actions.UpdateActions));
        AddBind(ConsoleKey.E, c);

        // Equiping
        c.Add(new ActionChainNode(() => _model.Player.Inventory.Items.Count > 0, () => Logger.Instance.Message("You have no items!")));
        c.Add(new ActionChainNode(() => PlayerActions.DoEquip(_model, _view)));
        c.Add(new ActionChainNode(_view.Actions.UpdateActions));
        AddBind(ConsoleKey.I, c);

        // Dequiping
        c.Add(new ActionChainNode(() =>
        {
            var leftHand = _model.Player.Inventory.LeftHand;
            var rightHand = _model.Player.Inventory.RightHand;

            return leftHand != null || rightHand != null;
        },
            () => Logger.Instance.Message("You aren't holding anything!")
        ));
        c.Add(new ActionChainNode(_model.Player.Inventory.CanAddItem, () => Logger.Instance.Message("Your inventory is full!")));
        c.Add(new ActionChainNode(() => PlayerActions.DoDequip(_model, _view)));
        c.Add(new ActionChainNode(_view.Actions.UpdateActions));
        AddBind(ConsoleKey.F, c);

        // Program behaviours
        c.Add(new ActionChainNode(() =>
        {
            Console.Clear();
            Environment.Exit(0);
        }
        ));
        AddBind(ConsoleKey.Escape, c);
    }
}
