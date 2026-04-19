using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Unit", menuName = "Config/Unit", order = 0)]
    public class UnitConfigs : ScriptableObject
    {
        [field: SerializeField] public UnitMovement Movement { get; private set; }
        [field: SerializeField] public UnitStats Stats { get; private set; }
        [field: SerializeField] public UnitAnimation Animation { get; private set; }
        [field:SerializeField] public UnitType Type { get; private set; }
        [field:SerializeField] public GameObject UnitModel { get; private set; }
    }

    [Serializable]
    public class UnitMovement
    {
        public float speed;
        public float acceleration;
    }
    
    [Serializable]
    public class UnitStats
    {
        public int health;
        public int attack;
        public int defense;
    }

    [Serializable]
    public class UnitAnimation
    {
        public string walk;
        public string death;
        public string idle;
    }

    public enum UnitType
    {
        Range,
        Mile
    }
}