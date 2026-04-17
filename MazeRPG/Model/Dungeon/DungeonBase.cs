using StudentEXE.Interfaces;
using StudentEXE.Model.Pickables.Items.Holdables.Weapons;

namespace StudentEXE.Model.Dungeon;

internal abstract class DungeonBase
{
    private const int InitWidth = 40;
    private const int InitHeight = 20;

    public int Width { get; private set; }
    public int Height { get; private set; }
    protected Tile[,] _tiles; // [x, y]

    public DungeonBase(int width = InitWidth, int height = InitHeight)
    {
        Width = width;
        Height = height;
        _tiles = new Tile[width, height];

        Initialize();
    }

    protected abstract void Initialize();
    public bool CanStandAt(int x, int y)
    {
        return !_tiles[x, y].IsWall;
    }

    public Tile this[int x, int y] => _tiles[x, y];

    public abstract void GenerateCorridors();

    public void DeleteDeadEnds(int percentage)
    {
        Random rng = new();

        for (int y = 0; y < Height; y += 2)
        {
            for (int x = 0; x < Width; x += 2)
            {
                var current = _tiles[x, y];

                if (CountNeighbourWalls(current) == 3)
                {
                    var walls = GetNeighbourWalls(current);
                    Tile toRemove = walls[rng.Next(walls.Count)];

                    if (rng.Next(100) < percentage)
                    {
                        toRemove.IsWall = false;
                    }
                }
            }
        }
    }

    private int CountNeighbourWalls(Tile tile)
    {
        int x = tile.X;
        int y = tile.Y;
        int counter = 0;

        if (y == 0 || _tiles[x, y - 1].IsWall)          counter++; // top
        if (y == Height - 1 || _tiles[x, y + 1].IsWall) counter++; // bottom
        if (x == 0 || _tiles[x - 1, y].IsWall)          counter++; // left
        if (x == Width - 1 || _tiles[x + 1, y].IsWall)  counter++; // right

        return counter;
    }

    private List<Tile> GetNeighbourWalls(Tile tile)
    {
        int x = tile.X;
        int y = tile.Y;
        List<Tile> neighbours = new();

        if (y != 0 && _tiles[x, y - 1].IsWall)          neighbours.Add(_tiles[x, y - 1]); // top
        if (y != Height - 1 && _tiles[x, y + 1].IsWall) neighbours.Add(_tiles[x, y + 1]); // bottom
        if (x != 0 && _tiles[x - 1, y].IsWall)          neighbours.Add(_tiles[x - 1, y]); // left
        if (x != Width - 1 && _tiles[x + 1, y].IsWall)  neighbours.Add(_tiles[x + 1, y]); // right

        return neighbours;
    }

    public void GenerateRoom(Tile startTile, int size)
    {
        int endX = Math.Min(startTile.X + size, Width);
        int endY = Math.Min(startTile.Y + size, Height);

        // interior
        for (int y = startTile.Y; y < endY; y++)
        {
            for (int x = startTile.X; x < endX; x++)
            {
                _tiles[x, y].IsWall = false;
            }
        }
    }

    public void GenerateCentralHall(int width, int height)
    {
        if (width > Width) width = Width;
        if (height > Height) height = Height;

        int startX = (Width - width) / 2;
        int startY = (Height - height) / 2;

        for (int y = startY; y < startY + height; y++)
        {
            for (int x = startX; x < startX + width; x++)
            {
                _tiles[x, y].IsWall = false;
            }
        }
    }

    public void AddPickable(int x, int y, IPickable pickable)
    {

    }

    public void AddWeapon(int x, int y, WeaponBase weapon)
    {

    }
}
