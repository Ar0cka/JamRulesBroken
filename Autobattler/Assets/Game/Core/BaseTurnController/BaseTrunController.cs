using System.Collections.Generic;
using BattleSystem;
using Cysharp.Threading.Tasks;
using Game.Core.BaseUnits;
using Game.PatternCombat.BattleUnitSystem;
using Game.PatternCombat.TrunControllers;
using Game.PatternCombat.Units;
using UnityEngine;
using Zenject;

namespace Game.Core.BaseTurnController
{
    public abstract class BaseTurnController : ITurnUI
    {
        [Inject] protected PathService PathService;
        [Inject] protected UnitsRegister Units;
        
        protected Queue<BaseUnitController> UnitsQueue = new();

        public int CurrentTurn => TurnCount;
        protected int TurnCount = 0;

        protected bool IsTurn = false;
        protected bool IsPlayerTurn = false;
        

        public abstract void InitializeTurnController();

        public abstract UniTask Turn(IUnitRegister register);

        public virtual void CreateTurn()
        {
            UnitsQueue.Clear();
            
            var units = GetAllUnits();

            units.Sort((a, b) => b.GetUnitInfo().WorldInfo.unitConfig.Stats.initiative
                .CompareTo(a.GetUnitInfo().WorldInfo.unitConfig.Stats.initiative));
            
            foreach (var unit in units)
            {
                UnitsQueue.Enqueue(unit);
            }
            
            TurnCount++;

            IsPlayerTurn = true;
            IsTurn = false;
        }

        public int UnitsCount() => UnitsQueue.Count;
        public bool IsTurnActive() => IsTurn;

        public void PlayerTurnIsEnd() => IsPlayerTurn = false;
        

        protected List<BaseUnitController> GetAllUnits()
        {
            List<BaseUnitController> units = new();
            units.AddRange(Units.GetUnits(UnitParent.Player).Values);
            units.AddRange(Units.GetUnits(UnitParent.Enemy).Values);

            return units;
        }
    }

    public interface ITurnUI
    {
        public int CurrentTurn { get;}
    }
}