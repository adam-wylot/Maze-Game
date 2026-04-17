using StudentEXE.Model.Dungeon;

namespace StudentEXE.Model;

internal class InstructionsBuilder : IInitDungeonBuilder, IDungeonBuilder
{
    private List<string> _instruction = new();


    private void Reset()
    {
        _instruction = new();
    }

    public IDungeonBuilder BuildEmptyDungeon()
    {
        _instruction.Add("Thou art but a humble scholar of the Warsaw University of Technology...");
        _instruction.Add("Bereft of all remembrance of yesternight, thou awakenest in a place unknown to thee.");
        return this;
    }

    public IDungeonBuilder BuildFilledDungeon() => BuildEmptyDungeon();


    public IDungeonBuilder AddCentralHall(int width, int height)
    {
        _instruction.Add("From the south-east, the breath of a mighty void doth reach thine ears.");
        _instruction.Add("The many years dedicated to the noble sciences of Geodesy and Cartography, notwithstanding the absence of a tachymeter,");
        _instruction.Add($"have permitted you to estimate the proportions of this great vacancy at {width}x{height}.");
        return this;
    }

    public IDungeonBuilder AddCorridors()
    {
        _instruction.Add("Before thee lieth a multitude of winding ways, wherein no semblance of order may be discerned.");
        return this;
    }

    public IDungeonBuilder AddPickables(int count)
    {
        _instruction.Add("Fellow students have preceded you in this place; their forsaken belongings lie scattered upon the ground,");
        _instruction.Add("available for you to collect and take into your possession.");
        return this;
    }

    public IDungeonBuilder AddRandomRooms(int percentage)
    {
        _instruction.Add("One findeth here certain apartments of an inscrutable nature;");
        _instruction.Add("most fortunately, no lectures are currently in progress for which you find yourself unpunctual.");
        return this;
    }

    public IDungeonBuilder AddWeapons(int count)
    {
        _instruction.Add("Thou knowest not what this domain may bring forth, yet by good fortune,");
        _instruction.Add("there lie implements wherewith thou mayest shield the shreds of thy honor, so foully squandered yesternight.");
        return this;
    }

    public IDungeonBuilder AddEnemies(int count)
    {
        _instruction.Add("There reach thine ears the sounds of sundry creatures, which may well pose a most grievous peril…");
        return this;
    }

    public List<string> GetResult()
    {
        var result = _instruction;
        Reset();

        return result;
    }
}
