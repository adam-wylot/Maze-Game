using StudentEXE.Model.Pickables;
using StudentEXE.Model.Entities.Enemies;
using StudentEXE.Model.Pickables.Items;
using StudentEXE.Model.Pickables.Items.Holdables.Modifiers;
using StudentEXE.Model.Pickables.Items.Holdables.Weapons;

namespace StudentEXE.Model.Dungeon;

internal class MapBuilder : IInitDungeonBuilder, IDungeonBuilder
{
    private const int MaxGold = 3;
    private const int MaxCoins = 50;

    private DungeonBase? _dungeon = null;
    private List<Tile>? _freeTiles = null;

    private bool _hasCorridors = false;
    private (int width, int height) _centralHallSize = (0, 0);
    private int _pickablesCount = 0;
    private int _roomsPercentage = 0;
    private int _weaponsCount = 0;
    private int _enemiesCount = 0;

    private void Reset()
    {
        _dungeon = null;

        _hasCorridors = false;
        _centralHallSize = (0, 0);
        _pickablesCount = 0;
        _roomsPercentage = 0;
        _weaponsCount = 0;
        _enemiesCount = 0;
    }

    public IDungeonBuilder BuildEmptyDungeon()
    {
        _dungeon = new EmptyDungeon();
        return this;
    }

    public IDungeonBuilder BuildFilledDungeon()
    {
        _dungeon = new FilledDungeon();
        return this;
    }



    public IDungeonBuilder AddCorridors()
    {
        _hasCorridors = true;
        return this;
    }

    public IDungeonBuilder AddCentralHall(int width, int height)
    {
        _centralHallSize = (width, height);
        return this;
    }

    public IDungeonBuilder AddPickables(int count)
    {
        _pickablesCount += count;
        return this;
    }

    public IDungeonBuilder AddRandomRooms(int percentage)
    {
        _roomsPercentage = percentage;
        return this;
    }

    public IDungeonBuilder AddWeapons(int count)
    {
        _weaponsCount += count;
        return this;
    }

    public IDungeonBuilder AddEnemies(int count)
    {
        _enemiesCount += count;
        return this;
    }

    public DungeonBase GetResult()
    {
        if (_dungeon == null)
        {
            Reset();
            throw new InvalidOperationException("There is no result dungeon. Dungeon doesn't exist!");
        }

        if (_hasCorridors) _dungeon.GenerateCorridors();
        if (_centralHallSize != (0, 0)) _dungeon.GenerateCentralHall(_centralHallSize.width, _centralHallSize.height);
        if (_roomsPercentage > 0) BuildRooms();
        if (_hasCorridors) _dungeon.DeleteDeadEnds(60);

        if (_pickablesCount > 0) PlacePickables();
        if (_weaponsCount > 0) PlaceWeapons();
        if (_enemiesCount > 0) SpawnEnemies();

        var result = _dungeon;
        Reset();

        return result;
    }




    private void BuildRooms()
    {
        if (_dungeon == null)
        {
            Reset();
            throw new InvalidOperationException("Can't generate rooms. Dungeon doesn't exist!");
        }

        List<(int x1, int y1, int x2, int y2)> areas = new();
        int endX;
        int endY;

        if (_centralHallSize != (0, 0))
        {
            int width = _centralHallSize.width;
            int height = _centralHallSize.height;
            int startX = (_dungeon.Width - width) / 2;
            int startY = (_dungeon.Height - height) / 2;
            endX = startX + width - 1;
            endY = startY + height - 1;

            areas.Add((startX, startY, endX, endY));
        }

        // generating rooms
        int perc = _roomsPercentage;
        Random rng = new();

        for (int y = 0; y < _dungeon.Height; y += 2)
        {
            for (int x = 0; x < _dungeon.Width; x += 2)
            {
                if (rng.Next(100) >= perc)
                {
                    continue;
                }

                int size = 3 + 2 * rng.Next(2);
                endX = Math.Min(x + size, _dungeon.Width) - 1;
                endY = Math.Min(y + size, _dungeon.Height) - 1;

                // if collides with other area
                bool status = true;

                foreach (var area in areas)
                {
                    if (!(endX < area.x1 || x > area.x2 || endY < area.y1 || y > area.y2))
                    {
                        status = false;
                        break;
                    }
                }

                if (status)
                {
                    // adding room
                    areas.Add((x, y, endX, endY));

                    _dungeon.GenerateRoom(_dungeon[x, y], size);
                }
            }
        }
    }

    private void PlacePickables()
    {
        if (_dungeon == null)
        {
            Reset();
            throw new InvalidOperationException("Can't place items. Dungeon doesn't exist!");
        }

        // Collecting free spaces
        var freeTiles = GetFreeTiles();

        if (freeTiles.Count == 0)
        {
            Reset();
            throw new InvalidOperationException("Can't place items. There is no free tiles in the dungeon!");
        }

        // Placing items
        Random rng = new();
        for (int i = 0; i < _pickablesCount; i++)
        {
            if (rng.Next(2) == 1)
            {
                // currencies
                if (rng.Next(2) == 1)
                {
                    // coins
                    var tile = freeTiles[rng.Next(freeTiles.Count)];
                    tile.PutItem(new Coins(rng.Next(MaxCoins) + 1));
                }
                else
                {
                    // gold
                    var tile = freeTiles[rng.Next(freeTiles.Count)];
                    tile.PutItem(new Gold(rng.Next(MaxGold) + 1));
                }
            }
            else
            {
                // item
                var tile = freeTiles[rng.Next(freeTiles.Count)];
                var item = ItemsDatabase.UnusableItems[rng.Next(ItemsDatabase.UnusablesCount)];

                int modifierChance = 50;

                var availableModifiers = Enumerable.Range(0, ModifiersDatabase.ItemModifiers.Count).ToList();

                while (rng.Next(100) < modifierChance && availableModifiers.Count > 0)
                {
                    int listIndex = rng.Next(availableModifiers.Count);
                    int modifierIndex = availableModifiers[listIndex];

                    var randomModifierFunc = ModifiersDatabase.ItemModifiers[modifierIndex];
                    item = randomModifierFunc(item);

                    availableModifiers.RemoveAt(listIndex);

                    modifierChance /= 2;
                }

                tile.PutItem(item);
            }
        }
    }

    private void PlaceWeapons()
    {
        if (_dungeon == null)
        {
            Reset();
            throw new InvalidOperationException("Can't place weapons. Dungeon doesn't exist!");
        }

        // Placing weapons
        Random rng = new();
        var freeTiles = GetFreeTiles();

        if (freeTiles.Count == 0)
        {
            Reset();
            throw new InvalidOperationException("Can't place weapons. There is no free tiles in the dungeon!");
        }

        for (int i = 0; i < _weaponsCount; i++)
        {
            var tile = freeTiles[rng.Next(freeTiles.Count)];
            var weaponTemplate = WeaponsDatabase.All[rng.Next(WeaponsDatabase.Count)];

            int modifierChance = 100;

            var availableModifiers = Enumerable.Range(0, ModifiersDatabase.WeaponModifiers.Count).ToList();

            while (rng.Next(100) < modifierChance && availableModifiers.Count > 0)
            {
                int listIndex = rng.Next(availableModifiers.Count);
                int modifierIndex = availableModifiers[listIndex];

                var randomModifierFunc = ModifiersDatabase.WeaponModifiers[modifierIndex];
                weaponTemplate = randomModifierFunc(weaponTemplate);

                availableModifiers.RemoveAt(listIndex);

                modifierChance /= 2;
            }

            tile.PutItem(weaponTemplate);
        }
    }

    private List<Tile> GetFreeTiles()
    {
        if (_freeTiles != null)
        {
            return _freeTiles;
        }

        if (_dungeon == null)
        {
            _freeTiles = new List<Tile>();
            return _freeTiles;
        }

        List<Tile> freeTiles = new();
        for (int y = 0; y < _dungeon.Height; y++)
        {
            for (int x = 0; x < _dungeon.Width; x++)
            {
                if (!_dungeon[x, y].IsWall)
                {
                    freeTiles.Add(_dungeon[x, y]);
                }
            }
        }

        _freeTiles = freeTiles;
        return freeTiles;
    }

    private void SpawnEnemies()
    {
        Random rng = new();
        var freeTiles = GetFreeTiles();
        var avaibleTiles = Enumerable.Range(0, freeTiles.Count).ToList();

        if (freeTiles.Count < _enemiesCount)
        {
            Reset();
            throw new InvalidOperationException("Can't spawn enemies. There is no enough free tiles in the dungeon!");
        }

        for (int i = 0; i < _enemiesCount; i++)
        {
            var tileIndex = rng.Next(avaibleTiles.Count);
            avaibleTiles.RemoveAt(tileIndex);

            var tile = freeTiles[tileIndex];
            var enemy = EnemiesDatabase.All[rng.Next(EnemiesDatabase.Count)];
            tile.AddEnemy(enemy);
        }
    }
}
