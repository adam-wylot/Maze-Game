using StudentEXE.Enumerators;
using StudentEXE.Model.Dungeon;
using StudentEXE.Model.Entities;

namespace StudentEXE.Model;

internal class GameModel
{
    private DungeonBase _dungeon;
    private Player _player = new();

    public DungeonBase Dungeon => _dungeon;
    public Player Player => _player;



    public GameModel(Action<IInitDungeonBuilder> builderFunc)
    {
        MapBuilder mb = new();

        builderFunc(mb);
        _dungeon = mb.GetResult();
    }



    // player methods
    public bool MovePlayerUp()
    {
        int pX = _player.X;
        int pY = _player.Y;

        if (pY <= 0 || !_dungeon.CanStandAt(pX, pY - 1))
        {
            return false;
        }

        _player.Move(Direction.Up);
        return true;
    }

    public bool MovePlayerDown()
    {
        int pX = _player.X;
        int pY = _player.Y;

        if (pY >= _dungeon.Height - 1 || !_dungeon.CanStandAt(pX, pY + 1))
        {
            return false;
        }

        _player.Move(Direction.Down);
        return true;
    }

    public bool MovePlayerLeft()
    {
        int pX = _player.X;
        int pY = _player.Y;

        if (pX <= 0 || !_dungeon.CanStandAt(pX - 1, pY))
        {
            return false;
        }

        _player.Move(Direction.Left);
        return true;
    }

    public bool MovePlayerRight()
    {
        int pX = _player.X;
        int pY = _player.Y;

        if (pX >= _dungeon.Width - 1 || !_dungeon.CanStandAt(pX + 1, pY))
        {
            return false;
        }

        _player.Move(Direction.Right);
        return true;
    }

    public InstructionsSet GetAvaibleActions()
    {
        int pX = _player.X;
        int pY = _player.Y;

        return _dungeon[pX, pY].GetTileActions() | _player.GetAvaibleActions();
    }
}
