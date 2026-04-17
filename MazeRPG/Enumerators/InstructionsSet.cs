namespace StudentEXE.Enumerators;

[Flags]
internal enum InstructionsSet : uint
{
    None = 0,
    Movement = 1 << 0,
    Pickuping = 1 << 1,
    Dropping = 1 << 2,
    Equiping = 1 << 3,
    Dequiping = 1 << 4,
    Attacking = 1 << 5,
}
