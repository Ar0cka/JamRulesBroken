using System;
using System.Linq;
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
            var enemyList = register.GetUnits(unitController.GetEnemyType());
            var target = unitController.ChooseTarget(enemyList.Values.ToList());
            
            await unitController.Action(PathService, target);

            IsPlayerTurn = true;
            
            IsTurn = false;
        }
    }
}