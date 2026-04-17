using StudentEXE.Model.AttackHandling;
using StudentEXE.Model.Entities;

namespace StudentEXE.Model.Pickables.Items.Holdables.Weapons;

internal class HeavyWeapon : WeaponBase
{
    public HeavyWeapon(string name = "Unknown Heavy Weapon", int damage = 0, bool isTwoHanded = true) : base(name, damage, isTwoHanded) {}

    public override string ItemTypeName => "Heavy";

    public override CombatResult PerformAttack(IAttackVisitor visitor, Player player, HoldableItem? context = null)
    {
        return visitor.Visit(this, context ?? this, player);
    }
}
