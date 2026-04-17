using StudentEXE.Enumerators;
using StudentEXE.Model.Entities;

namespace StudentEXE.Interfaces;

internal interface IEquipable
{
    public bool Equip(Player player, EquipmentSlot slot);
    public bool Dequip(Player player);
}
