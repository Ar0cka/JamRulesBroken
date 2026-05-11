using System.Collections.Generic;
using BattleSystem;
using DefaultNamespace.Pathfiender;
using Game.Combat.Grid;
using Game.PatternCombat.BattleUnitSystem;
using Game.PatternCombat.TrunControllers;
using Grid;
using UnityEngine;
using Zenject;

namespace Game.Core.BaseTurnController
{
    public abstract class BaseTurnController : MonoBehaviour
    {
        [Inject] protected PathService PathService;
        [Inject] protected UnitsRegister Units;
        
        protected Queue<UnitController> UnitsQueue;

        public int CurrenTurn { get; private set; } = 0;

        protected bool IsPlayerTurn = false;
        protected bool IsUnitTurn = false;

        public abstract void InitializeTurnController();

        protected virtual void CreateTurn()
        {
            var units = GetAllUnits();
            
            units.Sort((a, b) => 
                a.GetData().WorldInfo.unitConfig.Stats.initiative.CompareTo(b.GetData().WorldInfo.unitConfig.Stats.initiative));
            
            foreach (var unit in units)
            {
                UnitsQueue.Enqueue(unit);
            }
            
            CurrenTurn++;

            IsPlayerTurn = true;
            IsUnitTurn = true;
        }

        protected List<UnitController> GetAllUnits()
        {
            List<UnitController> units = new List<UnitController>();
            units.AddRange(Units.GetUnits(UnitParent.Player).Values);
            units.AddRange(Units.GetUnits(UnitParent.Enemy).Values);

            return units;
        }
    }
}