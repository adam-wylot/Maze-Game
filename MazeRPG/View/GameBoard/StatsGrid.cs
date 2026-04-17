using StudentEXE.Model;
using StudentEXE.Model.Pickables.Items;
using StudentEXE.Model.Pickables.Items.Holdables;
using StudentEXE.Model.Entities;

namespace StudentEXE.View.Game;

internal class StatsGrid : RendererBase
{
    private int _attributesRow = 0;
    private int _resourcesRow = 0;
    private int _inventoryRow = 0;
    private int _handsRow = 0;

    public void Draw(Player player)
    {
        int i = 0;

        DrawLine(HeavyLine, i++);
        DrawCenter(player.Name, i++);
        DrawLine(HeavyLine, i++);
        DrawAttributes(player, ref i);
        DrawLine(HeavyLine, i++);
        DrawInventory(player, ref i);
        DrawLine(HeavyLine, i++);
        _handsRow = i;
        DrawLeft("Left Hand:", i++);
        UpdateLeftHand(player.Inventory.LeftHand);
        i++;
        DrawLine(LightLine, i++);
        DrawLeft("Right Hand:", i++);
        UpdateRightHand(player.Inventory.RightHand);
        i++;
        DrawLine(HeavyLine, i++);
        _resourcesRow = i;
        UpdateGold(player.Inventory.Gold);
        UpdateCoins(player.Inventory.Coins);
        i += 2;
        DrawLine(HeavyLine, i++);
    }

    private void DrawAttributes(Player player, ref int i)
    {
        DrawCenter("Attributes:", i++);
        DrawLine(LightLine, i++);
        _attributesRow = i;
        UpdateAttributes(player);
        i += 6;
    }

    public void UpdateAttributes(Player player)
    {
        UpdateHealth(player.Health);
        UpdateStrength(player.Strength);
        UpdateAgility(player.Agility);
        UpdateLuck(player.Luck);
        UpdateAggression(player.Aggression);
        UpdateWisdom(player.Wisdom);
    }

    private void DrawInventory(Player player, ref int i)
    {
        DrawCenter("Inventory:", i++);
        DrawLine(LightLine, i++);
        _inventoryRow = i;

        UpdateInventory(player.Inventory.Items);
        i += Inventory.Capacity;
    }

    public void UpdateHealth(int value)
    {
        DrawLeft($"Health: {value}", _attributesRow);
    }

    public void UpdateStrength(int value)
    {
        DrawLeft($"Strength: {value}", _attributesRow + 1);
    }

    public void UpdateAgility(int value)
    {
        DrawLeft($"Agility: {value}", _attributesRow + 2);
    }

    public void UpdateLuck(int value)
    {
        DrawLeft($"Luck: {value}", _attributesRow + 3);
    }

    public void UpdateAggression(int value)
    {
        DrawLeft($"Aggression: {value}", _attributesRow + 4);
    }

    public void UpdateWisdom(int value)
    {
        DrawLeft($"Wisdom: {value}", _attributesRow + 5);
    }

    public void UpdateGold(int value)
    {
        DrawLeft($"Gold: {value}", _resourcesRow);
    }

    public void UpdateCoins(int value)
    {
        DrawLeft($"Coins: {value}", _resourcesRow + 1);
    }

    public void UpdateInventory(List<ItemBase> items)
    {
        for (int i = 0; i < Inventory.Capacity; i++)
        {
            UpdateInventoryAt(i, items.Count > i ? items[i] : null);
        }
    }

    public void UpdateInventoryAt(int index, ItemBase? item)
    {
        string prefixNumber = (index + 1).ToString() + ". ";
        DrawLeft(prefixNumber + (item != null ? item.ToString() : "-/-"), _inventoryRow + index);
    }

    public void UpdateInventoryAt(int index, string? name)
    {
        string prefixNumber = (index + 1).ToString() + ". ";
        DrawLeft(prefixNumber + name ?? "???", _inventoryRow + index);
    }

    public void UpdateLeftHand(HoldableItem? item)
    {
        DrawLeft("   " + (item != null ? item.ToString() : "-/-"), _handsRow + 1);
    }

    public void UpdateRightHand(HoldableItem? item)
    {
        DrawLeft("   " + (item != null ? item.ToString() : "-/-"), _handsRow + 4);
    }
}
