using StudentEXE.Enumerators;
using StudentEXE.Interfaces;
using StudentEXE.Model.AttackHandling;
using StudentEXE.Model.Entities;

namespace StudentEXE.Model.Pickables.Items.Holdables;

internal class HoldableItem : ItemBase, IHoldable
{
    protected bool _isTwoHanded = false;
    public override bool IsTwoHanded => _isTwoHanded;
    public virtual int Damage { get; protected set; } = 0;
    public override char Symbol => 'I';

    public HoldableItem(string name)
    {
        Name = name;
    }

    public HoldableItem(HoldableItem obj) : base(obj)
    {
        _isTwoHanded = obj.IsTwoHanded;
    }

    public override bool Equip(Player player, EquipmentSlot slot)
    {
        if (IsTwoHanded)
        {
            return player.Inventory.EquipBothHands(this);
        }

        switch (slot)
        {
            case EquipmentSlot.LeftHand:
                return player.Inventory.EquipLeftHand(this);

            case EquipmentSlot.RightHand:
                return player.Inventory.EquipRightHand(this);

            default:
                Logger.Instance.Message("You can't place it here!");
                return false;
        }
    }

    public override bool Dequip(Player player)
    {
        if (player.Inventory.LeftHand == this)
        {
            return player.Inventory.Dequip(EquipmentSlot.LeftHand);
        }

        if (player.Inventory.RightHand == this)
        {
            return player.Inventory.Dequip(EquipmentSlot.RightHand);
        }

        return false;
    }

    public virtual void Use(Player player)
    {
        Logger.Instance.Message("This item is unusable.");
    }

    public CombatResult Attack(Player player, AttackType attackType)
    {
        IAttackVisitor visitor;

        switch (attackType)
        {
            default:
            case AttackType.Normal:
                {
                    visitor = new NormalAttack();
                    break;
                }
            case AttackType.Stealth:
                {
                    visitor = new StealthAttack();
                    break;
                }
            case AttackType.Magic:
                {
                    visitor = new MagicAttack();
                    break;
                }
        }

        return PerformAttack(visitor, player);
    }

    public virtual CombatResult PerformAttack(IAttackVisitor visitor, Player player, HoldableItem? context = null)
    {
        return visitor.Visit(this, context ?? this, player);
    }

    public virtual void ApplyStats(Player player) { }
    public virtual void RemoveStats(Player player) { }
}
