using StudentEXE.Model.Pickables.Items.Holdables.Weapons;

namespace StudentEXE.Model.Pickables.Items.Holdables.Weapons;

internal static class WeaponsDatabase
{
    public static readonly LightWeapon Flail = new(
        "Flail",
        25,
        false
    );

    public static readonly LightWeapon Stick = new(
        "Stick",
        5,
        false
    );

    public static readonly LightWeapon TeleBaton = new(
        "Telescopic baton",
        20,
        false
    );



    public static readonly HeavyWeapon StopSign = new(
        "Stop Sign",
        80,
        true
    );



    public static readonly MagicWeapon PepperSpray = new(
        "Pepper Spray",
        15,
        false
    );

    public static readonly MagicWeapon StunGun = new(
        "Stun Gun",
        50,
        false
    );



    public static readonly List<WeaponBase> All =
    [
        Flail,
        StopSign,
        PepperSpray,
        Stick,
        TeleBaton,
        StunGun
    ];

    public static int Count => All.Count;
}
