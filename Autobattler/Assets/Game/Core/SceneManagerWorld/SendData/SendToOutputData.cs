using System;
using System.Collections.Generic;
using Game.Data.UnitConfigs;
using Game.PatternCombat;

namespace Game.Core.SceneManagerWorld.SendData
{
    [Serializable]
    public class SendToOutputData
    {
        public List<UnitWorldInfo> playerUnits;
        public FightResult fightResult;
    }
}