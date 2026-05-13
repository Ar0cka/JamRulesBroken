using System;
using Game.Data.UnitConfigs;
using UnityEngine;

namespace BattleSystem.UnitSystem.data
{
    public class UnitCombatInfo
    {
        public UnitCombatInfo(UnitWorldInfo worldInfo)
        {
            WorldInfo = worldInfo;
            CurrentEffectData = new EffectUnitData();
        }
        
        public UnitWorldInfo WorldInfo;
        
        public EffectUnitData CurrentEffectData;
        
        public Vector2 CurrentWorldPosition { get; private set; }

        public int X { get; private set; } = -1;
        public int Y { get; private set; } = -1;
        
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
            WorldInfo.unitCount = newCount;
        }
    }
}