using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Core.BaseTurnController;
using Game.PatternCombat.BattleUnitSystem;
using Game.PatternCombat.TrunControllers.TurnVariants;
using UnityEngine;
using Zenject;

namespace Game.PatternCombat.TrunControllers
{
    public class TurnManager : MonoBehaviour
    {
        [Inject] private TurnFactory _turnFactory;
        [Inject] private IUnitRegister _unitRegister;
        
        private Dictionary<TurnControllerType, BaseTurnController> _turnControllers = new();
        
        private TurnControllerType _currentControllerType;
        private BaseTurnController _currentController;

        public void InitializeTurnManager(ref Action<TurnControllerType> onChangeType, ref Action endTurn)
        {
            onChangeType += ChangedControllerType;
            _currentControllerType = TurnControllerType.Manual;

            _turnControllers[TurnControllerType.Manual] = 
                _turnFactory.CreateTurnController<ManualTurnController>();
            
            _currentController = _turnControllers[_currentControllerType];

            endTurn += () =>
            {
                _currentController.PlayerTurnIsEnd();
                
                ControlTurn().Forget(ex =>
                {
                    Debug.Log(ex.Message);
                });
            };
        }
        
        private void ChangedControllerType(TurnControllerType type)
        {
            _currentControllerType = type;

            if (_turnControllers.TryGetValue(type, out var value))
            {
                _currentController = value;
            }
        }

        private async UniTask ControlTurn()
        {
            while (_currentController.IsTurnActive())
            {
                await _currentController.Turn(_unitRegister);
            }
        }
    }

    public enum TurnControllerType
    {
        Manual,
        ArmyTemplates,
        Automatic
    }
}