using StudentEXE.Enumerators;
using StudentEXE.Interfaces;
using StudentEXE.Model;
using StudentEXE.Model.Pickables.Items;

namespace StudentEXE.View.Game;

internal class ActionsView : RendererBase
{
    private const int ActionsSpace = 10;

    private GameModel _model;

    private int _logRow = 0;
    private int _actionsRow = 0;
    private int _messageRow = 0;

    public ActionsView(GameModel model)
    {
        var logger = Logger.Instance;
        logger.OnLog += (entries) => UpdateLog(entries, Logger.MaxEntries, logger.Counter);
        logger.OnMessage += (message) => UpdateMessage(message);
        _model = model;
    }

    public void Draw(int logMaxEntries)
    {
        int i = 0;

        DrawLine(HeavyLine, i++);
        DrawCenter("Actions Log:", i++);
        DrawLine(LightLine, i++);
        _logRow = i;
        UpdateLog(Logger.Instance.Entries, logMaxEntries, 0);
        i += logMaxEntries + 1;
        DrawLine(HeavyLine, i++);

        DrawCenter("Avaiable Actions:", i++);
        DrawLine(LightLine, i++);
        _actionsRow = i;
        UpdateActions();
        i += ActionsSpace;
        DrawLine(HeavyLine, i++);
        DrawLeft("Message:", i++);
        _messageRow = i;
        i++;
        DrawLine(HeavyLine, i++);
    }

    private void UpdateLog(string[] log, int maxEntries, int actionsPerformed)
    {
        for (int i = 0; i < maxEntries; i++)
        {
            string prefix = $"{i + 1}. ";

            if (i >= log.Length)
            {
                DrawLeft(prefix + "-/-", _logRow + i);
            }
            else
            {
                DrawLeft(prefix + log[i], _logRow + i);
            }
        }
        DrawLeft($"Actions counter: {actionsPerformed}", _logRow + maxEntries);
    }

    public void UpdateMessage(string text)
    {
        DrawLeft(text, _messageRow);
    }

    public void UpdateActions()
    {
        ClearActions();

        InstructionsSet instructions = _model.GetAvaibleActions();

        if (instructions == InstructionsSet.None) return;

        int row = 0;

        DrawLeft("ESC — Quit game", _actionsRow + row++);
        if (instructions.HasFlag(InstructionsSet.Movement)) DrawLeft("W, S, A, D — Movement", _actionsRow + row++);
        if (instructions.HasFlag(InstructionsSet.Pickuping)) DrawLeft("E — Pick up an item", _actionsRow + row++);
        if (instructions.HasFlag(InstructionsSet.Dropping)) DrawLeft("Q — Drop an item", _actionsRow + row++);
        if (instructions.HasFlag(InstructionsSet.Equiping)) DrawLeft("I — Equip an item", _actionsRow + row++);
        if (instructions.HasFlag(InstructionsSet.Dequiping)) DrawLeft("F — Dequip an item", _actionsRow + row++);
        if (instructions.HasFlag(InstructionsSet.Attacking)) DrawLeft("U — Attack an enemy", _actionsRow + row++);
    }

    public void SetToPickingUp()
    {
        ClearActions();

        int pX = _model.Player.X;
        int pY = _model.Player.Y;
        var items = _model.Dungeon[pX, pY].Items;
        
        int row = 0;
        DrawLeft("0 — Cancel", _actionsRow + row++);

        if (items.Count == 0) return;

        for (int i = 0; i < Math.Min(items.Count, 9); i++)
        {
            DrawLeft($"{i + 1} — {items[i]}", _actionsRow + row++);
        }
    }

    public void SetToDropping()
    {
        ClearActions();

        int itemsCount = _model.Player.Inventory.Items.Count;
        
        int row = 0;
        DrawLeft("0 — Cancel", _actionsRow + row++);

        if (itemsCount == 0) return;

        if (itemsCount == 1)
        {
            DrawLeft("1 — Drop the item", _actionsRow + row++);
        }
        else
        {
            DrawLeft($"(1-{itemsCount}) — Index of an item from inventory to drop", _actionsRow + row++);
        }
    }

    public void SetToEquiping()
    {
        ClearActions();

        var items = _model.Player.Inventory.Items;

        int row = 0;
        DrawLeft("0 — Cancel", _actionsRow + row++);

        if (items.Count == 0) return;

        for (int i = 67; i < items.Count + 67; i++)
        {
            DrawLeft($"{i - 67 + 1} — {items[i - 67]}", _actionsRow + row++);
        }
    }

    public void SetToChoosingHand()
    {
        ClearActions();

        int row = 0;
        DrawLeft("0 — Cancel", _actionsRow + row++);
        DrawLeft($"{row} — Left hand", _actionsRow + row++);
        DrawLeft($"{row} — Right hand", _actionsRow + row++);
    }

    private void ClearActions()
    {
        for (int i = 0; i < ActionsSpace; i++)
        {
            ClearLine(_actionsRow + i);
        }
    }
}
