using StudentEXE.Model.Pickables.Items.Holdables;
using StudentEXE.Model.Entities;
using StudentEXE.Model.Pickables.Items.Holdables.Weapons;

namespace StudentEXE.Model.AttackHandling;

internal interface IAttackVisitor
{
    CombatResult Visit(HeavyWeapon weapon, HoldableItem context, Player player);
    CombatResult Visit(LightWeapon weapon, HoldableItem context, Player player);
    CombatResult Visit(MagicWeapon weapon, HoldableItem context, Player player);
    CombatResult Visit(HoldableItem? item, HoldableItem? context, Player player);
}
