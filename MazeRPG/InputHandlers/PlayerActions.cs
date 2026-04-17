using StudentEXE.Enumerators;
using StudentEXE.Interfaces;
using StudentEXE.Model;
using StudentEXE.Model.Pickables.Items;
using StudentEXE.View.Game;
using StudentEXE.Scenes;

namespace StudentEXE.InputHandlers;

internal static class PlayerActions
{
    public static void DoMove(GameModel model, MapView view, int dx = 0, int dy = 0)
    {
        var oldCell = model.Dungeon[model.Player.X - dx, model.Player.Y - dy];
        var newCell = model.Dungeon[model.Player.X, model.Player.Y];

        // view update
        view.Dungeon.MovePlayer(oldCell, newCell);

        Logger.Instance.Log($"Moved up to ({newCell.X}, {newCell.Y}).");
    }
    
    public static void DoDrop(GameModel model, MapView view)
    {
        ItemBase? item = null;
        var player = model.Player;


        // there is more items -- player need to choose one
        view.Actions.SetToDropping();
        Logger.Instance.Message("Choose an item to drop it.");

        int index = 0;
        do
        {
            index = InputHandlerBase.WaitForNumericInput();

            if (index == 0)
            {
                return;
            }

            item = model.Player.DropItem(index - 1);

        } while (item == null);


        // dropping the item
        int pX = player.X;
        int pY = model.Player.Y;
        Tile tile = model.Dungeon[pX, pY];

        tile.PutItem(item!);

        Logger.Instance.Log($"Dropped the item (" + item!.GetName() + ").");
        view.Actions.UpdateMessage("");

        view.Stats.UpdateInventory(model.Player.Inventory.Items);
    }

    public static void DoPickup(GameModel model, MapView view)
    {
        int pX = model.Player.X;
        int pY = model.Player.Y;
        var tile = model.Dungeon[pX, pY];
        IPickable? item;
        int index;

        // player have to choose an item to pick it up
        view.Actions.SetToPickingUp();
        Logger.Instance.Message("Choose an item to pick up.");

        do
        {
            index = InputHandlerBase.WaitForNumericInput();

            if (index == 0)
            {
                return;
            }

            item = tile.RemoveItem(index - 1);

        } while (item == null);


        // picking up the item
        item.OnPickUp(model.Player);

        view.Stats.UpdateInventory(model.Player.Inventory.Items);
        view.Stats.UpdateGold(model.Player.Inventory.Gold);
        view.Stats.UpdateCoins(model.Player.Inventory.Coins);

        Logger.Instance.Log($"Picked up the item (" + item.GetName() + ").");
        view.Actions.UpdateMessage("");
    }

    public static void DoEquip(GameModel model, MapView view)
    {
        var items = model.Player.Inventory.Items;
        view.Actions.SetToEquiping();
        Logger.Instance.Message("Choose an item to equip it.");

        // waiting for player input to choose an item
        ItemBase? item = null;
        int index;
        do
        {
            index = InputHandlerBase.WaitForNumericInput();

            if (index == 0)
            {
                return;
            }

            if (index <= items.Count)
            {
                item = model.Player.Inventory.Items[index - 1];
            }

        } while (item == null);

        // selecting a hand
        bool rightHand = true;

        if (!item.IsTwoHanded)
        {
            view.Actions.SetToChoosingHand();
            Logger.Instance.Message("Select a hand to equip an item to.");

            do
            {
                index = InputHandlerBase.WaitForNumericInput();

                if (index == 0)
                {
                    // cancel
                    return;
                }
            } while (index > 2);

            rightHand = index == 2; // 2 means player chosed right hand
        }

        if (item.Equip(model.Player, rightHand ? EquipmentSlot.RightHand : EquipmentSlot.LeftHand) == false)
        {
            Logger.Instance.Message("No space in the inventory for items in hands!");
        }
        else
        {
            view.Stats.UpdateRightHand(model.Player.Inventory.RightHand); // update right hand
            view.Stats.UpdateLeftHand(model.Player.Inventory.LeftHand); // update left hand
            view.Stats.UpdateInventory(model.Player.Inventory.Items);
            view.Stats.UpdateAttributes(model.Player);

            Logger.Instance.Log($"Equiped an item (" + item.GetName() + ").");
            view.Actions.UpdateMessage("");
        }
    }

    public static void DoDequip(GameModel model, MapView view)
    {
        var leftHand = model.Player.Inventory.LeftHand;
        var rightHand = model.Player.Inventory.RightHand;
        ItemBase? item = null;

        if (leftHand == null || rightHand == null || (leftHand != null && rightHand != null && leftHand == rightHand))
        {
            // only one hand is used
            item = leftHand ?? rightHand;
        }
        else
        {
            // player have to choose which hand he wants to be freed
            view.Actions.SetToChoosingHand();
            Logger.Instance.Message("Choose which item you want to dequip.");

            int index = 0;
            do
            {
                index = InputHandlerBase.WaitForNumericInput();

                if (index == 0)
                {
                    return;
                }

                if (index <= 2)
                {
                    if (index == 1)
                    {
                        item = leftHand;
                    }
                    else
                    {
                        item = rightHand;
                    }
                }

            } while (item == null);
        }

        // dequiping the item
        item!.Dequip(model.Player);

        view.Stats.UpdateRightHand(model.Player.Inventory.RightHand); // update right hand
        view.Stats.UpdateInventory(model.Player.Inventory.Items);
        view.Stats.UpdateLeftHand(model.Player.Inventory.LeftHand); // update left hand
        view.Stats.UpdateAttributes(model.Player);

        Logger.Instance.Log($"Dequiped an item (" + item!.GetName() + ").");
        view.Actions.UpdateMessage("");
    }

    public static bool CheckNoEnemy(GameModel model)
    {
        var player = model.Player;
        var ceil = model.Dungeon[player.X, player.Y];

        if (ceil.TileEnemy != null)
        {
            return false;
        }

        return true;
    }

    public static void PerformCombat(Game game, GameModel model, MapView view)
    {
        var player = model.Player;
        var ceil = model.Dungeon[player.X, player.Y];

        if (ceil.TileEnemy == null) return;

        var enemy = ceil.TileEnemy;

        Action onPlayerDead = () => {
            game.PopScene();
            game.PopScene();
        };

        Action onCombatEnd = () => {
            game.PopScene();
        };

        game.PushScene(new CombatScene(model, view, player, enemy, onPlayerDead, onCombatEnd));
    }
}
