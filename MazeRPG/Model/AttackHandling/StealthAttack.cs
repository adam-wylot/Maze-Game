using StudentEXE.Model.Pickables.Items.Holdables;
using StudentEXE.Model.Entities;
using StudentEXE.Model.Pickables.Items.Holdables.Weapons;

namespace StudentEXE.Model.AttackHandling;

internal class StealthAttack : IAttackVisitor
{
    public CombatResult Visit(HeavyWeapon weapon, HoldableItem context, Player player)
    {
        return new CombatResult(
            Damage: context.Damage / 2,
            Defense: player.Strength
        );
    }

    public CombatResult Visit(LightWeapon weapon, HoldableItem context, Player player)
    {
        return new CombatResult(
            Damage: context.Damage * 2,
            Defense: player.Agility
        );
    }

    public CombatResult Visit(MagicWeapon weapon, HoldableItem context, Player player)
    {
        return new CombatResult(
            Damage: 1,
            Defense: 0
        );
    }

    public CombatResult Visit(HoldableItem? item, HoldableItem? context, Player player)
    {
        return new CombatResult(
            Damage: context?.Damage ?? 0,
            Defense: 0
        );
    }
}
