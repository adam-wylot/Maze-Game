using StudentEXE.Model.Pickables.Items.Holdables.Weapons;
using StudentEXE.Model.Entities;

namespace StudentEXE.Model.Pickables.Items.Holdables.Modifiers.Weapons;

internal class SharpModifier : WeaponModifier
{
    public SharpModifier(WeaponBase weapon) : base(weapon)
    {
        Name = weapon.Name + " (Sharp)";
    }

    public override int Damage => base.Damage + 3;

    public override void ApplyStats(Player player)
    {
        base.ApplyStats(player);
        player.Agility += 2;
    }

    public override void RemoveStats(Player player)
    {
        base.RemoveStats(player);
        player.Agility -= 2;
    }
}