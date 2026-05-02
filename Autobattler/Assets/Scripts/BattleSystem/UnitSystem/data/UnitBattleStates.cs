using System;
using ScriptableObjects;
using ScriptableObjects.UnitConfigs;
using UnityEngine;

namespace BattleSystem.UnitSystem.data
{
    [Serializable]
    public class UnitBattleStates
    {
        public UnitBattleStates(UnitConfig unitConfig, int count)
        {
            UnitConfig = unitConfig;
            Count = count;

            CurrentEffectData = new EffectUnitData();
        }
        
        public EffectUnitData CurrentEffectData;
        
        public Vector2 CurrentWorldPosition { get; private set; }

        public int X { get; private set; } = -1;
        public int Y { get; private set; } = -1;
        
        [field:SerializeField] public UnitConfig UnitConfig { get; private set; }
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

        public void SetNewCount(int newCount)
        {
            Count = newCount;
        }
    }
}