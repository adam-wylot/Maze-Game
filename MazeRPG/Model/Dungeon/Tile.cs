using StudentEXE.Enumerators;
using StudentEXE.Interfaces;
using StudentEXE.Model.Entities.Enemies;

namespace StudentEXE.Model;

internal class Tile
{
    private bool _isWall;
    private readonly List<IPickable> _items = [];
    private Enemy? _enemy = null;
    public bool IsWall 
    {
        get => _isWall;
        set
        {
            _isWall = value;

            if (value && _items.Count > 0)
            {
                _items.Clear();
            }
        }
    }

    public List<IPickable> Items => _items;
    public Enemy? TileEnemy => _enemy;

    public int X { get; private set; }
    public int Y { get; private set; }


    public Tile(int x, int y, bool isWall = false)
    {
        X = x;
        Y = y;
        _isWall = isWall;
    }

    public char? GetSymbol()
    {
        if (_enemy != null)
        {
            return _enemy.Symbol;
        }

        int count = _items.Count;

        if (count == 0)
        {
            return null;
        }

        if (count > 1)
        {
            return count.ToString()[0];
        }

        return _items[0].Symbol;
    }

    public void PutItem(IPickable item)
    {
        _items.Add(item); 
    }

    public IPickable? RemoveItem(int index)
    {
        if (index >= _items.Count || index < 0)
        {
            return null;
        }

        var item = _items[index];
        _items.RemoveAt(index);
        return item;
    }

    public InstructionsSet GetTileActions()
    {
        InstructionsSet avaibleActions = InstructionsSet.None;

        if (_items.Count > 0) avaibleActions |= InstructionsSet.Pickuping;
        if (_enemy != null) avaibleActions &= ~InstructionsSet.Pickuping;

        return avaibleActions;
    }

    public void AddEnemy(Enemy enemy)
    {
        if (_enemy != null)
        {
            return;
        }

        enemy.DeadCallback += OnEnemyDeath;
        _enemy = enemy;
    }

    private void OnEnemyDeath(Enemy enemy)
    {
        _enemy = null;
    }
}
