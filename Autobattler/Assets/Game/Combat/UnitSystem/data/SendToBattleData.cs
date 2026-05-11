using System;
using System.Collections.Generic;
using BattleSystem;
using Game.Data.SpellConfigs;
using Game.Data.UnitConfigs;

namespace Game.Combat.UnitSystem.data
{
    [Serializable]
    public class SendToBattleData
    {
        public SendToBattleData(List<UnitWorldInfo> playerUnits, List<UnitWorldInfo> enemyUnits, List<SpellConfig> playerSpells)
        {
            this.playerUnits = playerUnits;
            this.enemyUnits = enemyUnits;
            this.playerSpells = playerSpells;
        }
        
        public List<UnitWorldInfo> playerUnits;
        public List<UnitWorldInfo> enemyUnits;
        public List<SpellConfig> playerSpells;
    }

    [Serializable]
    public class SendToOutputData
    {
        public List<UnitWorldInfo> playerUnits;
        public FightResult resultFight;
    }
}