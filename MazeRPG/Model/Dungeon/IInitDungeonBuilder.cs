namespace StudentEXE.Model.Dungeon;

internal interface IInitDungeonBuilder
{
    IDungeonBuilder BuildEmptyDungeon();
    IDungeonBuilder BuildFilledDungeon();
}
