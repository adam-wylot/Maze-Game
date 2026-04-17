using StudentEXE.Model;
using StudentEXE.Model.Dungeon;
using StudentEXE.Model.Entities;
using System.Text;

namespace StudentEXE.View.Game;

internal class MapView : RendererBase
{
    private GameModel _model;

    private StatsGrid _statsGrid = new();
    private DungeonGrid _dungeonGrid = new();
    private ActionsView _actionsGrid;

    public StatsGrid Stats => _statsGrid;
    public DungeonGrid Dungeon => _dungeonGrid;
    public ActionsView Actions => _actionsGrid;


    public MapView(GameModel model)
    {
        _model = model;
        _actionsGrid = new(model);

        _model.Player.OnPlayerMoved += HandlePlayerMoved;
    }

    public void Draw()
    {
        var dungeon = _model.Dungeon;
        var player = _model.Player;

        Console.Clear();

        // Generating Grid
        int dungeonSize = dungeon.Width + 2 + 2 * _dungeonGrid.Spacing; // +2 for borders
        int inventorySize = (Width - dungeonSize) / 2;
        int actionsSize = Width - dungeonSize - inventorySize;

        _statsGrid.Width = inventorySize;
        _statsGrid.Height = Height;
        _statsGrid.X = 0;

        _dungeonGrid.Width = dungeonSize;
        _dungeonGrid.Height = Height;
        _dungeonGrid.X = inventorySize;

        _actionsGrid.Width = actionsSize;
        _actionsGrid.Height = Height;
        _actionsGrid.X = inventorySize + dungeonSize;

        // Drawing
        //DrawLogo(inventorySize, room.Height + 2);
        _statsGrid.Draw(player);
        _dungeonGrid.Draw(dungeon, player);
        _actionsGrid.Draw(Logger.MaxEntries);
    }

    /*
    private void DrawLogo(int startX, int roomHeight)
    {
        int startY = (Height - roomHeight) / 2 - mazeLogo.Length - 1;

        for (int i = 0; i < mazeLogo.Length; i++)
        {
            DrawCenter(mazeLogo[i], startY + i);
        }

        startY = (Height - roomHeight) / 2 + roomHeight + 1;

        for (int i = 0; i < rpgLogo.Length; i++)
        {
            DrawCenter(rpgLogo[i], startY + i);
        }
    }
    */

    private void HandlePlayerMoved(((int x, int y) from, (int x, int y) to) data)
    {
        var oldTail = _model.Dungeon[data.from.x, data.from.y];
        var newTail = _model.Dungeon[data.to.x, data.to.y];

        _dungeonGrid.MovePlayer(oldTail, newTail);
        _actionsGrid.UpdateActions();
    }
}
