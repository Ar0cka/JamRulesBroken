using System;
using Game.Core.SceneManagerWorld;
using Game.PatternCombat.BattleUnitSystem;
using Game.PatternCombat.CombatEndSystem;
using UnityEngine;
using Zenject;

namespace Game.PatternCombat
{
    public class CombatBootstrap : MonoBehaviour
    {
        [SerializeField] private InitializeBattleUnits unitsFactory;
        [SerializeField] private EndCombatUI endCombatUi;

        [Inject] private UnitsRegister _unitsRegister;
        
        private EndChecker _endChecker = new();
        
        private void Awake()
        {
            if (SwitchScene.Instance == null)
                throw new NullReferenceException();

            var units = SwitchScene.Instance.GetData();
            
            unitsFactory.CreateArmy(UnitParent.Player, units.playerUnits);
            unitsFactory.CreateArmy(UnitParent.Enemy, units.enemyUnits);
            
            _endChecker.SubscribeToRegister(_unitsRegister);
            endCombatUi.Initialize(ref _endChecker.OnEndCombat);
            
            //TODO Initialize Spell System
            //TODO Initialize Pattern System
            //TODO Create First turn queue
            
            //TODO Initialize Hud UI
            //TODO Initialize End Battle with saving send data for calculate lost units after fight
        }
    }
}