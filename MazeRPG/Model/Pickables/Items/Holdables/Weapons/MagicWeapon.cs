using StudentEXE.Model.AttackHandling;
using StudentEXE.Model.Entities;

namespace StudentEXE.Model.Pickables.Items.Holdables.Weapons;

internal class MagicWeapon : WeaponBase
{
    public MagicWeapon(string name = "Unknown Magic Weapon", int damage = 0, bool isTwoHanded = false) : base(name, damage, isTwoHanded)
    {
    }

    public override string ItemTypeName => "Magic";

    public override CombatResult PerformAttack(IAttackVisitor visitor, Player player, HoldableItem? context = null)
    {
        return visitor.Visit(this, context ?? this, player);
    }
}
