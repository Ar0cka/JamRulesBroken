using System;
using UnityEngine;

namespace ScriptableObjects.UnitConfigs
{
    [CreateAssetMenu(fileName = "Unit", menuName = "Config/Unit", order = 0)]
    public class UnitConfig : ScriptableObject
    {
        [field: SerializeField] public int UnitID { get; private set; }
        [field: SerializeField] public UnitData UnitData { get; private set; }
        [field: SerializeField] public UnitMovement Movement { get; private set; }
        [field: SerializeField] public UnitStats Stats { get; private set; }
        [field: SerializeField] public UnitAnimation Animation { get; private set; }
        [field: SerializeField] public UnitVisualData VisualData { get; private set; }
        
    }

    [Serializable]
    public class UnitData
    {
        public string unitName;
        public UnitType unitType;
    }

    [Serializable]
    public class UnitVisualData
    {
        public Sprite unitSprite;
        public GameObject unitModel;
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
        public int cellsInTurn;
        public int initiative;
    }

    [Serializable]
    public class UnitAnimation
    {
        public string walk;
        public string death;
        public string attack;
        public string hit;
    }

    public enum UnitType
    {
        Range,
        Mile
    }
}