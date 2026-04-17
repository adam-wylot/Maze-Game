using StudentEXE.Enumerators;
using StudentEXE.Model.Entities;

namespace StudentEXE.Model.Pickables.Items.Holdables.Weapons;

internal abstract class WeaponBase : HoldableItem
{
    public override char Symbol => 'W';

    public WeaponBase(string name = "Unknown Weapon", int damage = 0, bool isTwoHanded = false) : base(name)
    {
        Damage = damage;
        _isTwoHanded = isTwoHanded;
    }

    public WeaponBase(WeaponBase obj) : base(obj) 
    {
        Damage = obj.Damage;
    }

    public override bool Equip(Player player, EquipmentSlot slot)
    {
        bool success;

        if (IsTwoHanded)
        {
            success = player.Inventory.EquipBothHands(this);
        }
        else
        {
            switch (slot)
            {
                case EquipmentSlot.LeftHand:
                    success = player.Inventory.EquipLeftHand(this);
                    break;

                case EquipmentSlot.RightHand:
                    success = player.Inventory.EquipRightHand(this);
                    break;

                default:
                    Logger.Instance.Message("You can't place it here!");
                    return false;
            }
        }

        if (success)
        {
            ApplyStats(player);
        }

        return success;
    }

    public override string ToString()
    {
        return Name + $" (Dmg: {Damage}" + (IsTwoHanded ? ", Two-handed" : "") + ")";
    }
}
