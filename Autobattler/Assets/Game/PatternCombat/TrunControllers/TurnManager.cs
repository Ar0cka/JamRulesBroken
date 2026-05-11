using System;
using BattleSystem;
using Game.PatternCombat.TrunControllers.TurnVariants;
using UnityEngine;

namespace Game.PatternCombat.TrunControllers
{
    public class TurnManager : MonoBehaviour
    {
        [SerializeField] private ManualTurnController manualController;
        
        private TurnControllerType _currentTurnController;
        public Action<TurnControllerType> OnTypeChange; //TODO Add UI Class with subscribe to this event

        private void ChangedControllerType(TurnControllerType type)
        {
            _currentTurnController = type;
        }
    }

    public enum TurnControllerType
    {
        Manual,
        ArmyTemplates,
        Automatic
    }
}