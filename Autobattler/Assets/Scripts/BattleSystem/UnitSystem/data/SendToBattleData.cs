using System.Collections.Generic;
using BattleSystem.UnitSystem.data;
using ScriptableObjects;

namespace BattleSystem.UnitSystem
{
    public class SendToBattleData
    {
        public SendToBattleData(List<UnitData> playerUnits, List<UnitData> enemyUnits, List<SpellConfig> playerSpells)
        {
            PlayerUnits = playerUnits;
            EnemyUnits = enemyUnits;
            PlayerSpells = playerSpells;
        }
        
        public List<UnitData> PlayerUnits;
        public List<UnitData> EnemyUnits;
        public List<SpellConfig> PlayerSpells;
    }

    public class SendToOutputData
    {
        public List<UnitData> UnitData;
        public FightResult ResultFight;
    }
}