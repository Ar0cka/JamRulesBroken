using System;
using System.Collections.Generic;
using Game.Data.SpellConfigs;
using Game.Data.UnitConfigs;

namespace Game.Core.SceneManagerWorld.SendData
{
    [Serializable]
    public class SendToBattleData
    {
        public SendToBattleData(List<UnitWorldInfo> playerUnits, List<UnitWorldInfo> enemyUnits,
            List<SpellConfig> playerSpells)
        {
            this.playerUnits = playerUnits;
            this.enemyUnits = enemyUnits;
            this.playerSpells = playerSpells;
        }

        public List<UnitWorldInfo> playerUnits;
        public List<UnitWorldInfo> enemyUnits;
        public List<SpellConfig> playerSpells;
    }
}