using System;
using Game.Data.UnitConfigs;
using Grid;

namespace Game.PatternCombat.BattleUnitSystem
{
    [Serializable]
    public class UnitCombatInfo
    {
        public UnitWorldInfo UnitInfo { get; private set; }
        public UnitParent UnitParent { get; private set; }
        public int Count { get; private set; }
        
        public GridData UnitPosition { get; private set; }
        
        public UnitCombatInfo(UnitWorldInfo worldInfo, UnitParent parent)
        {
            UnitInfo = worldInfo;
            Count = worldInfo.unitCount;

            UnitParent = parent;
        }

        public void SetPosition(GridData gridData)
        {
            UnitPosition = gridData;
        }
    }
}