using StudentEXE.Model.Pickables.Items.Holdables.Weapons;
using StudentEXE.Model.Entities;

namespace StudentEXE.Model.Pickables.Items.Holdables.Modifiers.Weapons;

internal class CursedWeaponModifier : WeaponModifier
{
    public CursedWeaponModifier(WeaponBase weapon) : base(weapon)
    {
        Name = weapon.Name + " (Cursed)";
    }

    public override int Damage => base.Damage + 15;

    public override void ApplyStats(Player player)
    {
        base.ApplyStats(player);
        player.Luck -= 10;
    }

    public override void RemoveStats(Player player)
    {
        base.RemoveStats(player);
        player.Luck += 10;
    }
}