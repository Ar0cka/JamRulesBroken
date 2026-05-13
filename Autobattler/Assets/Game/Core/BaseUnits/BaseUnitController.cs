using System;
using System.Collections.Generic;
using System.Linq;
using BattleSystem;
using BattleSystem.UnitSystem.data;
using Cysharp.Threading.Tasks;
using Game.Combat.Grid;
using Game.PatternCombat.TrunControllers;
using Game.PatternCombat.Units;
using Grid;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Core.BaseUnits
{
    public abstract class BaseUnitController : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D rb2D;
        
        protected UnitCombatInfo UnitInfo;
        protected UnitParent Parent;

        public virtual void InitializeUnit(UnitCombatInfo info, UnitParent parent, GridData gridData)
        {
            UnitInfo = info;
            Parent = parent;
            
            IsValidComponents();
            
            UnitInfo.SetPosition(gridData.X, gridData.Y, new Vector2(gridData.WorldX, gridData.WorldY));
        }

        public abstract BaseUnitController ChooseTarget(List<BaseUnitController> enemyUnits);
        public abstract UniTask Action(IPathService pathService);

        protected virtual BaseUnitController CheckUnitRadius()
        {
            var unitConfig = UnitInfo.WorldInfo.unitConfig;
            
            var aroundList =
                Physics2D.OverlapCircleAll(rb2D.position, unitConfig.UnitChecker.aroundUnit, unitConfig.UnitChecker.targetLayer).ToList();

            if (aroundList.Count <= 0)
                return null;

            List<BaseUnitController> unitsPosition = new();

            foreach (var unitCollider in aroundList)
            {
                var unitController = unitCollider.GetComponent<BaseUnitController>();
                
                Sort(unitController, ref unitsPosition);
            }

            var unit = ChoosePriorityType(unitsPosition);

            return unit;
        }
        protected virtual BaseUnitController ChoosePriorityType(List<BaseUnitController> units)
        {
            var unitPriorityConfig = UnitInfo.WorldInfo.unitConfig.PrioritySettings;

            var filteredUnits = units.Where(e =>
                unitPriorityConfig.priorityType.Contains(e.UnitInfo.WorldInfo.unitConfig.UnitDefinition.unitType));

            var unit = filteredUnits.FirstOrDefault();

            if (unit is null)
                return units.First();
            
            return unit;
        }
        protected virtual void Sort(BaseUnitController unitController, ref List<BaseUnitController> unitsPosition)
        {
            if (unitsPosition.Count <= 0)
            {
                unitsPosition.Add(unitController);
                return;
            }

            int i = 0;
            
            var unitDistance = Vector2.Distance(unitController.transform.position, rb2D.position);
            
            while (i < unitsPosition.Count)
            {
                var currentDistance = Vector2.Distance(rb2D.position, unitsPosition[i].transform.position);
                
                if (currentDistance > unitDistance)
                {
                    unitsPosition.Add(null);

                    var saved = unitsPosition[i];
                    unitsPosition[i] = unitController;
                    
                    for (int j = i; j < unitsPosition.Count; j++)
                    {
                        (unitsPosition[j + 1], saved) = (saved, unitsPosition[j + 1]);
                    }

                    return;
                }

                i++;
            }
            
            unitsPosition.Add(unitController);
        } 

        protected void IsValidComponents()
        {
            if (rb2D is null)
                throw new NullReferenceException(nameof(rb2D));
        }

        public UnitCombatInfo GetUnitInfo() => UnitInfo;
    }
}