using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Core.BaseUnits;
using Game.PatternCombat.TrunControllers;

namespace Game.PatternCombat.Units
{
    public class UnitController : BaseUnitController
    {
        public override BaseUnitController ChooseTarget(List<BaseUnitController> enemyUnits)
        {
            var aroundEnemy = CheckUnitRadius();

            if (aroundEnemy != null)
                return aroundEnemy;

            var unit = ChoosePriorityType(enemyUnits);

            return unit;
        }

        public override UniTask Action(IPathService pathService, BaseUnitController targetUnit)
        {
            throw new System.NotImplementedException();
        }
    }
}