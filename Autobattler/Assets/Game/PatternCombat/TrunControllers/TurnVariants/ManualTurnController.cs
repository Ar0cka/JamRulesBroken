using System;
using Cysharp.Threading.Tasks;
using Game.Core.BaseTurnController;
using Game.PatternCombat.BattleUnitSystem;

namespace Game.PatternCombat.TrunControllers.TurnVariants
{
    public class ManualTurnController : BaseTurnController
    {
        private Action _onUnitTurn;
        
        public override void InitializeTurnController()
        {
            IsPlayerTurn = true;
        }
        public override async UniTask Turn(IUnitRegister register)
        {
            if (IsPlayerTurn || UnitsQueue.Count == 0 || IsTurn)
                return;
            
            IsTurn = true;

            var unitController = UnitsQueue.Dequeue();
            var target = unitController.ChooseTarget()

            IsPlayerTurn = true;
            
            IsTurn = false;
        }
    }
}