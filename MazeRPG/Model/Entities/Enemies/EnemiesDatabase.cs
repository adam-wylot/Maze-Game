namespace StudentEXE.Model.Entities.Enemies;

internal static class EnemiesDatabase
{
    public static readonly Enemy AcademicGuard = new(
        "Academic Guard",
        100,
        (from: 5, to: 10),
        10
    );

    public static readonly Enemy WildDog = new(
        "Wild Dog",
        25,
        (from: 2, to: 10),
        2
    );

    public static readonly Enemy Hangover = new(
        "Hangover",
        1000,
        (from: 50, to: 200),
        0
    );

    public static readonly Enemy Homeless = new(
        "Homeless",
        75,
        (from: 2, to: 15),
        5
    );



    public static readonly List<Enemy> All =
    [
        AcademicGuard,
        WildDog,
        Hangover,
        Homeless
    ];

    public static int Count => All.Count;
}
