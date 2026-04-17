using StudentEXE.Interfaces;
using StudentEXE.Model.Entities;

namespace StudentEXE.Model.Pickables;

internal class Coins : IPickable
{
    public int Amount { get; private set; }
    public char Symbol => 'C';

    public Coins(int amount)
    {
        Amount = amount;
    }

    public bool OnPickUp(Player player)
    {
        player.AddCoins(Amount);
        return true;
    }

    public string GetName() => $"{Amount}x Coins";

    public override string ToString() => GetName();
}
