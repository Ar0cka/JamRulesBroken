using System.Collections.Generic;
using Game.Data.UnitConfigs;
using Game.PatternCombat.Units;
using Grid;
using UnityEngine;
using Zenject;

namespace Game.PatternCombat.BattleUnitSystem
{
    public class InitializeBattleUnits : MonoBehaviour
    {
        [SerializeField] private GridSystem grid;
        
        [Inject] private UnitsRegister _register;
        
        public void CreateArmy(UnitParent parent, List<UnitWorldInfo> units)
        {
            foreach (var unit in units)
            {
                var unitController = CreateUnit(parent, unit);
                
                if (unitController is not null)
                    _register.AddUnit(parent, unitController);
            }
        }

        private UnitController CreateUnit(UnitParent parent, UnitWorldInfo unitInfo)
        {
            var spawnPoint = parent == UnitParent.Player ? grid.GetRandomPlayerCell() : grid.GetRandomEnemyCell();

            var unitStates = new UnitCombatInfo(unitInfo, parent);
            
            var unitObject = Instantiate(unitStates.UnitInfo.unitConfig.VisualData.unitModel);
            unitObject.transform.position = unitStates.UnitPosition.WorldPosition;
            unitObject.name = $"{unitObject.name}:{parent}";
            
            var unitController = unitObject.GetComponent<UnitController>();
            unitController.InitializeUnit(unitStates, parent, grid.GridData[spawnPoint.x, spawnPoint.y]);

            if (unitController.GetParentType() == UnitParent.Enemy)
            {
                unitController.GetComponent<SpriteRenderer>().flipX = true;
            }

            return unitController;
        }
    }
}