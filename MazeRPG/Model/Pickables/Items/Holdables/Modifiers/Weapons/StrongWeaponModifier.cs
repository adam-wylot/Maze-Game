using StudentEXE.Model.Pickables.Items.Holdables.Weapons;

namespace StudentEXE.Model.Pickables.Items.Holdables.Modifiers.Weapons;

internal class StrongWeaponModifier : WeaponModifier
{
    public StrongWeaponModifier(WeaponBase weapon) : base(weapon)
    {
        Name = weapon.Name + " (Strong)";
    }

    public override int Damage => base.Damage + 5;
}
