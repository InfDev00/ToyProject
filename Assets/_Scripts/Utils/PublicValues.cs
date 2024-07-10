using Entities;
using UnityEngine;

namespace Utils
{
    public static class Tags
    {
        public const string ENEMY = "Enemy";
        public const string PLAYER = "Player";
        public const string OBJECT = "Object";
    }

    public static class Values
    {
        public const string ANIM_ATTACK = "Attack";
        public const string ANIM_ATTACK_SPEED = "AttackSpeed";
    }

    public enum WeaponType
    {
        RANGE_ATTACK,
        MELEE_ATTACK,
    }
}