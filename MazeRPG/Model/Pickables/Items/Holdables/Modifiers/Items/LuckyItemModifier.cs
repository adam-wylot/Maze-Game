using StudentEXE.Model.Entities;

namespace StudentEXE.Model.Pickables.Items.Holdables.Modifiers.Items;

internal class LuckyItemModifier : ItemModifier
{
    public LuckyItemModifier(HoldableItem item) : base(item)
    {
        Name = _wrappedItem.Name + " (Lucky)";
    }

    public override void ApplyStats(Player player)
    {
        base.ApplyStats(player);
        player.Luck += 5;
    }

    public override void RemoveStats(Player player)
    {
        base.RemoveStats(player);
        player.Luck -= 5;
    }
}
