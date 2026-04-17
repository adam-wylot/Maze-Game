namespace StudentEXE.Model.Dungeon;

internal class EmptyDungeon : DungeonBase
{
    protected override void Initialize()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                _tiles[x, y] = new Tile(x, y);
            }
        }
    }

    public override void GenerateCorridors()
    {
        // DFS
        bool[,] visited = new bool[Width, Height];
        bool[,] changed = new bool[Width, Height];
        Stack<Tile> stack = new();
        Random rng = new();

        Tile current = _tiles[0, 0];
        visited[0, 0] = true;
        stack.Push(current);

        while (stack.Count > 0)
        {
            current = stack.Pop();
            var unvisitedNeighbors = GetUnvisitedNeighbors(current, visited);

            if (unvisitedNeighbors.Count > 0)
            {
                stack.Push(current);
                Tile next = unvisitedNeighbors[rng.Next(unvisitedNeighbors.Count)];
                RemoveWalls(current, next, changed);
                visited[next.X, next.Y] = true;
                stack.Push(next);
            }
        }
    }

    private List<Tile> GetUnvisitedNeighbors(Tile tile, bool[,] visited)
    {
        List<Tile> neighbors = new();
        int x = tile.X;
        int y = tile.Y;

        if (y > 1 && !visited[x, y - 2]) neighbors.Add(_tiles[x, y - 2]); // Up
        if (x < Width - 2 && !visited[x + 2, y]) neighbors.Add(_tiles[x + 2, y]); // Right
        if (y < Height - 2 && !visited[x, y + 2]) neighbors.Add(_tiles[x, y + 2]); // Bottom
        if (x > 1 && !visited[x - 2, y]) neighbors.Add(_tiles[x - 2, y]); // Left

        return neighbors;
    }

    private void RemoveWalls(Tile tile1, Tile tile2, bool[,] changed)
    {
        CreateWallsAround(tile1, changed);
        CreateWallsAround(tile2, changed);

        _tiles[(tile1.X + tile2.X) / 2, (tile1.Y + tile2.Y) / 2].IsWall = false; // remove wall beetwen
    }

    private void CreateWallsAround(Tile tile, bool[,] changed)
    {
        int x;
        int y;

        for (int biasY = -1; biasY <= 1; biasY++)
        {
            for (int biasX = -1; biasX <= 1; biasX++)
            {
                x = tile.X + biasX;
                y = tile.Y + biasY;

                if (x >= 0 && x < Width && y >= 0 && y < Height && !changed[x,y])
                {
                    _tiles[x, y].IsWall = true;
                    changed[x, y] = true;
                }
            }
        }

        tile.IsWall = false;
    }
}
