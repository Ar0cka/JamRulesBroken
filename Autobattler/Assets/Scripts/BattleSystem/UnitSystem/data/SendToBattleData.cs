using System.Collections.Generic;
using BattleSystem.UnitSystem.data;
using ScriptableObjects;
using ScriptableObjects.SpellConfigs;

namespace BattleSystem.UnitSystem
{
    public class SendToBattleData
    {
        public SendToBattleData(List<UnitBattleStates> playerUnits, List<UnitBattleStates> enemyUnits, List<SpellConfig> playerSpells)
        {
            PlayerUnits = playerUnits;
            EnemyUnits = enemyUnits;
            PlayerSpells = playerSpells;
        }
        
        public List<UnitBattleStates> PlayerUnits;
        public List<UnitBattleStates> EnemyUnits;
        public List<SpellConfig> PlayerSpells;
    }

    public class SendToOutputData
    {
        public List<UnitBattleStates> UnitData;
        public FightResult ResultFight;
    }
}