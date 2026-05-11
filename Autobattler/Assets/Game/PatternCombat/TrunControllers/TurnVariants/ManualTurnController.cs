using System;
using Game.Combat.Grid;
using Game.Core.BaseTurnController;

namespace Game.PatternCombat.TrunControllers.TurnVariants
{
    public class ManualTurnController : BaseTurnController
    {
        private Action _onUnitTurn;
        
        public override void InitializeTurnController()
        {
            CreateTurn();

            IsPlayerTurn = true;
        }
    }
}