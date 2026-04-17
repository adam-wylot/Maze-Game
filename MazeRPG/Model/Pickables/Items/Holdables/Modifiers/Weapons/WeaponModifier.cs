using StudentEXE.Model.Pickables.Items.Holdables.Weapons;
using StudentEXE.Model.AttackHandling;
using StudentEXE.Model.Entities;

namespace StudentEXE.Model.Pickables.Items.Holdables.Modifiers.Weapons;

internal class WeaponModifier : WeaponBase
{
    protected WeaponBase _wrappedWeapon;

    protected WeaponModifier(WeaponBase weapon) : base(weapon)
    {
        _wrappedWeapon = weapon;
    }

    public override string ItemTypeName => _wrappedWeapon.ItemTypeName;

    public override CombatResult PerformAttack(IAttackVisitor visitor, Player player, HoldableItem? context = null)
    {
        return _wrappedWeapon.PerformAttack(visitor, player, context ?? this);
    }

    public override void ApplyStats(Player player)
    {
        _wrappedWeapon.ApplyStats(player);
    }

    public override void RemoveStats(Player player)
    {
        _wrappedWeapon.RemoveStats(player);
    }
}
