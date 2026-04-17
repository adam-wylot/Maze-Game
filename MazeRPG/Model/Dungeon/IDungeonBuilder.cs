namespace StudentEXE.Model.Dungeon;

internal interface IDungeonBuilder
{
    IDungeonBuilder AddCorridors();
    IDungeonBuilder AddRandomRooms(int percentage);
    IDungeonBuilder AddCentralHall(int width, int height);
    IDungeonBuilder AddPickables(int count);
    IDungeonBuilder AddWeapons(int count);
    IDungeonBuilder AddEnemies(int count);
}
