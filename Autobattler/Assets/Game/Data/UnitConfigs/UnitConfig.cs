using System;
using System.Collections.Generic;
using Game.Data.Patterns;
using UnityEngine;

namespace Game.Data.UnitConfigs
{
    [CreateAssetMenu(fileName = "Unit", menuName = "Config/Unit", order = 0)]
    public class UnitConfig : ScriptableObject
    {
        [field: SerializeField] public string UnitID { get; private set; }
        [field: SerializeField] public UnitDefinition UnitDefinition { get; private set; }
        [field: SerializeField] public UnitMovement Movement { get; private set; }
        [field: SerializeField] public UnitStats Stats { get; private set; }
        [field: SerializeField] public UnitAnimation Animation { get; private set; }
        [field: SerializeField] public UnitVisualData VisualData { get; private set; }
        
        [field: SerializeField] public List<UnitPattern> UnitPatterns { get; private set; }
        [field: SerializeField] public UnitChecker UnitChecker { get; private set; }
        
        [field: SerializeField] public UnitPrioritySettings PrioritySettings { get; private set; }
        
    }
    
    [Serializable]
    public class UnitDefinition
    {
        public string unitName;
        public string unitDescription;
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
        public int actionPoints;
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

    [Serializable]
    public class UnitChecker
    {
        public float aroundUnit;
        public float cellRadius;

        public LayerMask targetLayer;
    }

    [Serializable]
    public class UnitPrioritySettings
    {
        public List<UnitType> priorityType;
    }

    public enum UnitType
    {
        Range,
        Mile,
        Tank,
        Mage
    }
}