using StudentEXE.Model.Entities;

namespace StudentEXE.Interfaces;

internal interface IHoldable : IEquipable
{
    public bool IsTwoHanded { get; }
    public void Use(Player player);
}