using StudentEXE.Interfaces;
using StudentEXE.Model.Entities;

namespace StudentEXE.Model.Pickables;

internal class Gold : IPickable
{
    public int Amount { get; private set; }
    public char Symbol => 'G';

    public Gold(int amount)
    {
        Amount = amount;
    }

    public bool OnPickUp(Player player)
    {
        player.AddGold(Amount);
        return true;
    }

    public string GetName() => $"{Amount}x Gold";

    public override string ToString() => GetName();
}
