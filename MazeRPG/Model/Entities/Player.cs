using StudentEXE.Enumerators;
using StudentEXE.Model.Pickables.Items;
using StudentEXE.Model.Pickables.Items.Holdables;
using StudentEXE.Model.AttackHandling;

namespace StudentEXE.Model.Entities;

internal class Player
{
    private const int BaseHealth = 100;
    private const int BaseStrength = 10;
    private const int BaseAgility = 10;
    private const int BaseLuck = 10;
    private const int BaseAggression = 10;
    private const int BaseWisdom = 10;

    public event Action<((int x, int y) from, (int x, int y) to)>? OnPlayerMoved;

    private int _hp = BaseHealth;
    private int _maxHp = BaseHealth;

    public string Name { get; private set; } = "MiNI Student 1";
    public int X { get; private set; } = 0;
    public int Y { get; private set; } = 0;
    public int Health
    {
        get => _hp;
        set
        {
            _hp = Math.Max(0, Math.Min(value, _maxHp));
        }
    }
    public int Strength { get; set; } = BaseStrength;
    public int Agility { get; set; } = BaseAgility;
    public int Luck { get; set; } = BaseLuck;
    public int Aggression { get; set; } = BaseAggression;
    public int Wisdom { get; set; } = BaseWisdom;

    public Inventory Inventory { get; private set; } = new();


    public void Move(Direction direction)
    {
        var oldPos = (X, Y);

        switch (direction)
        {
            case Direction.Up:
                Y--;
                break;

            case Direction.Down:
                Y++;
                break;

            case Direction.Left:
                X--;
                break;

            case Direction.Right:
                X++;
                break;
        }

        OnPlayerMoved?.Invoke((oldPos, (X, Y)));
    }

    public bool PickUpItem(ItemBase item)
    {
        return Inventory.AddItem(item);
    }

    public ItemBase? DropItem(int index)
    {
        return Inventory.RemoveItem(index);
    }

    public void AddGold(int amount) => Inventory.AddGold(amount);

    public void AddCoins(int amount) => Inventory.AddCoins(amount);

    public InstructionsSet GetAvaibleActions()
    {
        InstructionsSet set = InstructionsSet.Movement;

        if (Inventory.Items.Count > 0) set |= InstructionsSet.Equiping | InstructionsSet.Dropping;
        if (Inventory.LeftHand != null ||  Inventory.RightHand != null) set |= InstructionsSet.Dequiping;

        return set;
    }

    public CombatResult Attack(EquipmentSlot slot, AttackType attackType)
    {
        HoldableItem? weapon;

        switch (slot)
        {
            case EquipmentSlot.LeftHand:
            {
                weapon = Inventory.LeftHand;
                break;
            }
            case EquipmentSlot.RightHand:
            {
                weapon = Inventory.RightHand;
                break;
            }
            default:
                throw new Exception("Can't do it!");
        }

        if (weapon == null)
        {
            return new CombatResult(0, 0);
        }

        return weapon.Attack(this, attackType);
    }
}
