using StudentEXE.Enumerators;
using StudentEXE.Model.Pickables.Items;
using StudentEXE.Model.Pickables.Items.Holdables;

namespace StudentEXE.Model;

internal class Inventory
{
    public const int Capacity = 9;
    public List<ItemBase> Items { get; private set; } = [];
    public HoldableItem? LeftHand { get; private set; } = null;
    public HoldableItem? RightHand { get; private set; } = null;

    public int Coins { get; private set; } = 0;
    public int Gold { get; private set; } = 0;

    public bool EquipLeftHand(HoldableItem item)
    {
        int index = Items.IndexOf(item);
        Items.Remove(item);

        if (!Dequip(EquipmentSlot.LeftHand, index))
        {
            Items.Insert(index, item);
            return false;
        }

        LeftHand = item;
        return true;
    }
    
    public bool EquipRightHand(HoldableItem item)
    {
        int index = Items.IndexOf(item);
        Items.Remove(item);

        if (!Dequip(EquipmentSlot.RightHand, index))
        {
            Items.Insert(index, item);
            return false;
        }

        RightHand = item;
        return true;
    }

    public bool EquipBothHands(HoldableItem item)
    {
        int itemsToDequip = 0;

        if (LeftHand != null) itemsToDequip++;
        if (RightHand != null && RightHand != LeftHand) itemsToDequip++;

        if (Items.Count - 1 + itemsToDequip > Capacity)
        {
            // no space to dequip actually hold items
            Logger.Instance.Message("Not enough space to unequip items from hands!");
            return false;
        }

        int index = Items.IndexOf(item);
        Items.Remove(item);

        Dequip(EquipmentSlot.LeftHand, index);
        Dequip(EquipmentSlot.RightHand, index);

        LeftHand = item;
        RightHand = item;

        return true;
    }

    public bool Dequip(EquipmentSlot slot, int index = -1)
    {
        if (slot == EquipmentSlot.LeftHand || slot == EquipmentSlot.RightHand)
        {
            HoldableItem? item = slot == EquipmentSlot.LeftHand ? LeftHand : RightHand;

            if (item == null)
            {
                // no item -- no need to dequip
                return true;
            }

            // check if there is a space in inventory
            if (!CanAddItem())
            {
                return false;
            }

            // dequip item
            if (slot == EquipmentSlot.LeftHand || item.IsTwoHanded)
            {
                // left hand
                LeftHand = null;
            }
            if (slot == EquipmentSlot.RightHand || item.IsTwoHanded)
            {
                // right hand
                RightHand = null;
            }

            if (index == -1)
            {
                AddItem(item);
            }
            else
            {
                Items.Insert(index, item);
            }
        }
        else
        {
            // armor
            throw new NotImplementedException();
        }

        return true;
    }

    public bool AddItem(ItemBase item)
    {
        if (Items.Count >= Capacity)
        {
            return false;
        }

        Items.Add(item);
        return true;
    }

    public ItemBase? RemoveItem(int index)
    {
        if (Items.Count <= index)
        {
            return null;
        }

        var item = Items[index];
        Items.RemoveAt(index);
        return item;
    }

    public void AddGold(int amount) => Gold += amount;
    public void AddCoins(int amount) => Coins += amount;

    public bool CanAddItem() => Items.Count < Capacity;
}
