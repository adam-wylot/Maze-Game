using StudentEXE.Enumerators;
using StudentEXE.Interfaces;
using StudentEXE.Model.Entities;

namespace StudentEXE.Model.Pickables.Items;

internal abstract class ItemBase : IPickable, IEquipable
{
    public string Name { get; protected set; } = "Unknown item";
    public virtual char Symbol => '?';
    public virtual string ItemTypeName => "Plain item";
    public virtual bool IsTwoHanded => false;

    public ItemBase() { }
    public ItemBase(ItemBase obj)
    {
        Name = obj.Name;
    }


    public abstract bool Equip(Player player, EquipmentSlot slot);
    public abstract bool Dequip(Player player);
    public bool OnPickUp(Player player) => player.PickUpItem(this);
    public string GetName() => Name;

    public override string ToString()
    {
        return Name;
    }
}
