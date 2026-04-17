using StudentEXE.Model.Pickables.Items.Holdables.Weapons;
using StudentEXE.Model.Entities;

namespace StudentEXE.Model.Pickables.Items.Holdables.Modifiers.Weapons;

internal class LuckyWeaponModifier : WeaponModifier
{
    public LuckyWeaponModifier(WeaponBase weapon) : base(weapon)
    {
        Name = weapon.Name + " (Lucky)";
    }

    public override void ApplyStats(Player player)
    {
        base.ApplyStats(player);
        player.Luck += 5;
    }

    public override void RemoveStats(Player player)
    {
        base.RemoveStats(player);
        player.Luck -= 5;
    }
}