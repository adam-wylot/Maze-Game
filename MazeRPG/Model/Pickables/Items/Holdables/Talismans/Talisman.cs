namespace StudentEXE.Model.Pickables.Items.Holdables.Talismans;

internal class Talisman : HoldableItem
{
    public Talisman(string name = "Unknow Talisman") : base(name) { }

    public Talisman(Talisman obj) : base(obj)
    { 
    }

    public override char Symbol => 'T';

    public override string ToString()
    {
        return Name;
    }
}
