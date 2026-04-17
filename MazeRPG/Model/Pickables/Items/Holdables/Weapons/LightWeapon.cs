using StudentEXE.Model.AttackHandling;
using StudentEXE.Model.Entities;

namespace StudentEXE.Model.Pickables.Items.Holdables.Weapons;

internal class LightWeapon : WeaponBase
{
    public LightWeapon(string name = "Unknown Light Weapon", int damage = 0, bool isTwoHanded = false) : base(name, damage, isTwoHanded)
    {
    }

    public override string ItemTypeName => "Light";

    public override CombatResult PerformAttack(IAttackVisitor visitor, Player player, HoldableItem? context = null)
    {
        return visitor.Visit(this, context ?? this, player);
    }
}
