using StudentEXE.Model.Pickables.Items.Holdables;
using StudentEXE.Model.Entities;
using StudentEXE.Model.Pickables.Items.Holdables.Weapons;

namespace StudentEXE.Model.AttackHandling;

internal class NormalAttack : IAttackVisitor
{
    public CombatResult Visit(HeavyWeapon weapon, HoldableItem context, Player player)
    {
        return new CombatResult(
            Damage: context.Damage,
            Defense: player.Strength + player.Luck
        );
    }

    public CombatResult Visit(LightWeapon weapon, HoldableItem context, Player player)
    {
        return new CombatResult(
            Damage: context.Damage,
            Defense: player.Agility + player.Luck
        );
    }

    public CombatResult Visit(MagicWeapon weapon, HoldableItem context, Player player)
    {
        return new CombatResult(
            Damage: 1,
            Defense: player.Agility + player.Luck
        );
    }

    public CombatResult Visit(HoldableItem? item, HoldableItem? context, Player player)
    {
        return new CombatResult(
            Damage: context?.Damage ?? 0,
            Defense: player.Agility
        );
    }
}
