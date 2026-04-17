using StudentEXE.Interfaces;
using StudentEXE.Model;
using StudentEXE.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
namespace StudentEXE.Model.Entities.Enemies;

internal class Enemy : IDrawable
{
    public Action<Enemy>? DeadCallback { get; set; }
    private int _hp;

    public string Name { get; private set; }
    public int Hp
    {
        get => _hp;
        private set
        {
            _hp = Math.Max(0, value);

            if (_hp == 0)
            {
                DeadCallback?.Invoke(this);
                Logger.Instance.Message($"{Name} has been defeated!");
            }
        }
    }
    public (int from, int to) Damage { get; private set; }
    public int Defense { get; private set; }

    public Enemy(string name, int hp, (int from, int to) damage, int defense)
    {
        Name = name;
        Hp = hp;
        Damage = damage;
        Defense = defense;
    }

    public override string ToString() => $"{Name} (HP: {Hp}, DMG: {Damage}, DEF: {Defense})";
    public char Symbol => 'E';

    public void TakeDamage(int damage)
    {
        int effectiveDamage = Math.Max(0, damage - Defense);
        Hp -= effectiveDamage;
        Logger.Instance.Message($"{Name} takes {effectiveDamage} damage! (HP left: {Hp})");
    }

    public void RevengeAttack(Player player, int playerDefense)
    {
        Random rng = new();
        int damage = Damage.from + rng.Next(Damage.to - Damage.from);
        int effectiveDamage = Math.Max(0, damage - playerDefense);

        player.Health -= effectiveDamage;
        Logger.Instance.Message($"{Name} attacked you for {effectiveDamage}!");
    }
}
