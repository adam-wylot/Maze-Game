using StudentEXE.Model.Pickables.Items.Holdables.Weapons;
using System.Collections;

namespace StudentEXE.Model.Pickables.Items.Holdables.Talismans;

internal static class TalismansDatabase
{
    public static readonly Talisman LuckyTalisman = new(
        "Lucky Talisman"
    );

    public static readonly List<Talisman> All =
    [
        LuckyTalisman
    ];

    public static int Count => All.Count;
}
