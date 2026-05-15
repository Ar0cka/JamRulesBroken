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
        [Inject] private IPathService _pathService;
        
        private Dictionary<TurnControllerType, ITurnController> _turnControllers = new();
        
        private TurnControllerType _currentControllerType;
        private ITurnController _currentController;

        public void InitializeTurnManager(ref Action<TurnControllerType> onChangeType, ref Action endTurn)
        {
            onChangeType += ChangedControllerType;
            _currentControllerType = TurnControllerType.Manual;

            _turnControllers[TurnControllerType.Manual] = 
                _turnFactory.CreateTurnController<ManualTurnController>(_unitRegister, _pathService);
            
            _currentController = _turnControllers[_currentControllerType];

            endTurn += () =>
            {
                _currentController.PlayerTurnIsEnd();
                
                _currentController.AwaitPlayerTurn().Forget(e =>
                {
                    Debug.Log(e.Message);
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
    }

    public enum TurnControllerType
    {
        Manual,
        ArmyTemplates,
        Automatic
    }
}