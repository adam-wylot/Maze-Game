using StudentEXE.Enumerators;
using StudentEXE.Model.Pickables.Items.Holdables;
using StudentEXE.Model.Entities;
using StudentEXE.Model.Entities.Enemies;

namespace StudentEXE.Model.AttackHandling;

internal class CombatManager
{
    private Player _player;
    private Enemy _enemy;

    private Action _playerDeadCallback;

    public CombatManager(Player player, Enemy enemy, Action playerDeadCallback)
    {
        _player = player;
        _enemy = enemy;
        _playerDeadCallback = playerDeadCallback;
    }

    public void Perform(EquipmentSlot slot, AttackType attackType)
    {
        // 1. Player's move
        var result = _player.Attack(slot, attackType);
        _enemy.TakeDamage(result.Damage);

        if (_enemy.Hp == 0)
        {
            return;
        }

        // 2. Enemy revange
        _enemy.RevengeAttack(_player, result.Defense);

        // (3. is player alive)
        if (_player.Health == 0)
        {
            _playerDeadCallback.Invoke();
        }
    }

    public void Perform(HoldableItem weapon, AttackType attackType)
    {
        // 1. Player's move
        var result = weapon.Attack(_player, attackType);
        _enemy.TakeDamage(result.Damage);

        if (_enemy.Hp == 0)
        {
            return;
        }

        // 2. Enemy revange
        _enemy.RevengeAttack(_player, result.Defense);

        // (3. is player alive)
        if (_player.Health == 0)
        {
            _playerDeadCallback.Invoke();
        }
    }
}
