using System.Collections.Generic;
using BattleSystem.UnitSystem.data;

namespace BattleSystem.UnitSystem
{
    public class SendToBattleData
    {
        public List<UnitData> PlayerUnits;
        public List<UnitData> EnemyUnits;
    }

    public class SendToOutputData
    {
        public List<UnitData> UnitData;
        public FightResult ResultFight;
    }
}