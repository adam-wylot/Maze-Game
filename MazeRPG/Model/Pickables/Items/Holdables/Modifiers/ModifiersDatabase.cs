using StudentEXE.Model.Pickables.Items.Holdables.Weapons;
using StudentEXE.Model.Pickables.Items.Holdables.Modifiers.Items;
using StudentEXE.Model.Pickables.Items.Holdables.Modifiers.Weapons;

namespace StudentEXE.Model.Pickables.Items.Holdables.Modifiers;

internal static class ModifiersDatabase
{
    public static readonly List<Func<HoldableItem, HoldableItem>> ItemModifiers =
    [
        (item) => new LuckyItemModifier(item)
    ];

    public static readonly List<Func<WeaponBase, WeaponBase>> WeaponModifiers =
    [
        (weapon) => new StrongWeaponModifier(weapon),
        (weapon) => new LuckyWeaponModifier(weapon),
        (weapon) => new SharpModifier(weapon),
        (weapon) => new CursedWeaponModifier(weapon)
    ];
}