using StudentEXE.Model.Entities;

namespace StudentEXE.Model.Pickables.Items.Holdables.Modifiers.Items;

internal abstract class ItemModifier : HoldableItem
{
    protected HoldableItem _wrappedItem;

    protected ItemModifier(HoldableItem item) : base(item)
    {
        _wrappedItem = item;
    }

    public override void ApplyStats(Player player)
    {
        _wrappedItem.ApplyStats(player);
    }

    public override void RemoveStats(Player player)
    {
        _wrappedItem.RemoveStats(player);
    }
}
