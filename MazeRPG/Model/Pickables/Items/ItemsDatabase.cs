using StudentEXE.Model.Pickables.Items.Holdables;
using StudentEXE.Model.Pickables.Items.Holdables.Talismans;
using StudentEXE.Model.Pickables.Items.Holdables.Weapons;

namespace StudentEXE.Model.Pickables.Items;

internal static class ItemsDatabase
{
    public static readonly HoldableItem CalculusBook = new(
        "Calculus 1"
    );

    public static readonly HoldableItem Bucket = new(
        "Bucket"
    );

    public static readonly HoldableItem Cigarettes = new(
        "Malboro Gold"
    );

    public static readonly HoldableItem SnusPack = new(
        "Velo Peppermint (●●●●○○)"
    );



    private static readonly List<HoldableItem> _holdableItems =
    [
        CalculusBook,
        Bucket,
        Cigarettes,
        SnusPack
    ];

    public static readonly List<HoldableItem> All = _holdableItems
        .Concat<HoldableItem>(WeaponsDatabase.All)
        .Concat(TalismansDatabase.All)
        .ToList();

    public static int AllCount => _holdableItems.Count + WeaponsDatabase.Count + TalismansDatabase.Count;

    public static readonly List<HoldableItem> UnusableItems = _holdableItems
        .Concat<HoldableItem>(TalismansDatabase.All)
        .ToList();

    public static int UnusablesCount => _holdableItems.Count + TalismansDatabase.Count;
}
