namespace StudentEXE.Model.Dungeon;

internal class DungeonDirector
{
    public void BuildStandard(IInitDungeonBuilder mapBuilder)
    {
        mapBuilder
            .BuildFilledDungeon()
            .AddCorridors()
            .AddCentralHall(11, 7)
            .AddPickables(20)
            .AddWeapons(10);
    }

    public void BuildWithRooms(IInitDungeonBuilder builder)
    {
        builder
            .BuildEmptyDungeon()
            .AddCorridors()
            .AddCentralHall(11, 7)
            .AddRandomRooms(5)
            .AddPickables(20)
            .AddWeapons(10);
    }

    public void BuildNoItems(IInitDungeonBuilder builder)
    {
        builder
            .BuildFilledDungeon()
            .AddCorridors()
            .AddCentralHall(11, 7);
    }

    public void BuildFullOption(IInitDungeonBuilder builder)
    {
        builder
            .BuildEmptyDungeon()
            .AddCorridors()
            .AddCentralHall(11, 7)
            .AddRandomRooms(5)
            .AddPickables(20)
            .AddWeapons(10)
            .AddEnemies(10);
    }
}
