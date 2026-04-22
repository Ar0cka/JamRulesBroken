using System;
using ScriptableObjects;
using UnityEngine;

namespace BattleSystem.UnitSystem.data
{
    [Serializable]
    public class UnitData
    {
        public UnitData(UnitConfigs unitConfig, int count)
        {
            UnitConfig = unitConfig;
            Count = count;

            CurrentEffectData = new EffectUnitData();
        }
        
        public EffectUnitData CurrentEffectData;
        
        public Vector2 CurrentWorldPosition { get; private set; }
        
        public int X { get; private set; }
        public int Y { get; private set; }
        
        [field:SerializeField] public UnitConfigs UnitConfig { get; private set; }
        [field:SerializeField] public int Count { get; private set; }
        
        public void SetPosition(int x, int y, Vector2 worldPosition)
        {
            CurrentWorldPosition = worldPosition;
            X = x;
            Y = y;
        }

        public void SetNewEffectData()
        {
            CurrentEffectData = new EffectUnitData();
        }
    }
}