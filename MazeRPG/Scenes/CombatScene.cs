using StudentEXE.Enumerators;
using StudentEXE.Model;
using StudentEXE.View.Game;
using StudentEXE.InputHandlers;
using StudentEXE.Interfaces;
using StudentEXE.Model.AttackHandling;
using StudentEXE.Model.Entities;
using StudentEXE.Model.Entities.Enemies;
using StudentEXE.View.Combat;

namespace StudentEXE.Scenes;

internal class CombatScene : IGameScene
{
    private Player _player;
    private Enemy _enemy;
    private CombatView _view;
    private CombatManager _combatManager;
    private CombatInputHandler _inputHandler;

    private Action _playerDeadCallback;
    private Action _endCombatCallback;

    private enum CombatMenuState { Action, Weapon, AttackType }
    private CombatMenuState _currentState = CombatMenuState.Action;

    private int _selectedIndex = 0;
    private EquipmentSlot _selectedSlot;

    private readonly string[] _mainOptions = { "Attack", "Flee" };
    private readonly string[] _attackTypeOptions = { "Normal Attack", "Stealth Attack", "Magic Attack", "Back" };
    private List<string> _weaponOptions = new List<string>();
    private List<EquipmentSlot> _weaponSlots = new List<EquipmentSlot>();

    private List<string> _combatLog = new List<string>();

    internal bool IsCombatOver { get; private set; } = false;

    public CombatScene(GameModel model, MapView view, Player player, Enemy enemy, Action playerDeadCallback, Action endCombatCallback)
    {
        _player = player;
        _enemy = enemy;
        _view = new CombatView();
        _playerDeadCallback = playerDeadCallback;
        _endCombatCallback = endCombatCallback;
        _combatManager = new CombatManager(_player, _enemy, () => { });
        _inputHandler = new CombatInputHandler(model, view, this);

        _combatLog.Add($"You encounter {_enemy.Name}!");
    }

    public void Enter()
    {
        Logger.Instance.OnMessage += OnLogMessage;
        _view.InitializeLayout();
        RenderAll();
    }

    public void Resume()
    {
        _view.InitializeLayout();
        RenderAll();
    }

    public void HandleInput(ConsoleKey key)
    {
        _inputHandler.ProcessInput(key);
    }

    internal void MenuUp()
    {
        string[] currentOptions = GetCurrentOptions();
        _selectedIndex--;
        if (_selectedIndex < 0) _selectedIndex = currentOptions.Length - 1;

        _view.MenuGrid.Update(currentOptions, _selectedIndex);
    }

    internal void MenuDown()
    {
        string[] currentOptions = GetCurrentOptions();
        _selectedIndex++;
        if (_selectedIndex >= currentOptions.Length) _selectedIndex = 0;

        _view.MenuGrid.Update(currentOptions, _selectedIndex);
    }

    internal void ExecuteAction()
    {
        if (_currentState == CombatMenuState.Action)
        {
            if (_selectedIndex == 0) // Attack
            {
                BuildWeaponMenu();
                _currentState = CombatMenuState.Weapon;
                _selectedIndex = 0;
            }
            else if (_selectedIndex == 1) // Flee
            {
                _combatLog.Insert(0, "You flee in panic!");
                FinishCombat();
                return;
            }
        }
        else if (_currentState == CombatMenuState.Weapon)
        {
            if (_selectedIndex == _weaponOptions.Count - 1) // Back
            {
                _currentState = CombatMenuState.Action;
                _selectedIndex = 0;
            }
            else
            {
                _selectedSlot = _weaponSlots[_selectedIndex];
                _currentState = CombatMenuState.AttackType;
                _selectedIndex = 0;
            }
        }
        else if (_currentState == CombatMenuState.AttackType)
        {
            if (_selectedIndex == 3) // Back
            {
                _currentState = CombatMenuState.Weapon;
                _selectedIndex = 0;
            }
            else
            {
                AttackType type = (AttackType)_selectedIndex;
                _combatManager.Perform(_selectedSlot, type);
                CheckEndConditions();

                _currentState = CombatMenuState.Action;
                _selectedIndex = 0;
            }
        }

        RenderAll();
    }

    private string[] GetCurrentOptions()
    {
        return _currentState switch
        {
            CombatMenuState.Action => _mainOptions,
            CombatMenuState.Weapon => _weaponOptions.ToArray(),
            CombatMenuState.AttackType => _attackTypeOptions,
            _ => _mainOptions
        };
    }

    private void BuildWeaponMenu()
    {
        _weaponOptions.Clear();
        _weaponSlots.Clear();

        var left = _player.Inventory.LeftHand;
        var right = _player.Inventory.RightHand;

        if (left != null)
        {
            _weaponOptions.Add($"L. Hand: {left.Name} [{left.ItemTypeName}]");
            _weaponSlots.Add(EquipmentSlot.LeftHand);
        }

        if (right != null && right != left)
        {
            _weaponOptions.Add($"R. Hand: {right.Name} [{right.ItemTypeName}]");
            _weaponSlots.Add(EquipmentSlot.RightHand);
        }

        if (left == null && right == null)
        {
            _weaponOptions.Add("Bare hands [No weapon]");
            _weaponSlots.Add(EquipmentSlot.RightHand);
        }

        _weaponOptions.Add("Back");
    }

    private void CheckEndConditions()
    {
        if (_enemy.Hp <= 0)
        {
            _combatLog.Insert(0, "\x1b[32mYou won the battle! Press any key...\x1b[0m");
            IsCombatOver = true;
        }
        else if (_player.Health <= 0)
        {
            _combatLog.Insert(0, "\x1b[31mYou have been defeated... Press any key.\x1b[0m");
            IsCombatOver = true;
        }
    }

    private void OnLogMessage(string message)
    {
        _combatLog.Insert(0, message);
    }

    private void RenderAll()
    {
        _view.EnemyGrid.Update(_enemy);
        _view.PlayerGrid.Update(_player);
        _view.MenuGrid.Update(GetCurrentOptions(), _selectedIndex);

        var logsToDraw = _combatLog.Take(8).ToArray();
        _view.LogGrid.Update(logsToDraw);
    }

    internal void FinishCombat()
    {
        Logger.Instance.OnMessage -= OnLogMessage;

        if (_player.Health <= 0)
        {
            _playerDeadCallback?.Invoke();
        }
        else
        {
            _endCombatCallback?.Invoke();
        }
    }
}