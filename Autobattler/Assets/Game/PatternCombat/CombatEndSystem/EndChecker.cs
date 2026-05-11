using System;
using System.Collections.Generic;
using System.Linq;
using BattleSystem;
using Game.Combat.UnitSystem.data;
using Game.Data.UnitConfigs;
using Game.PatternCombat.BattleUnitSystem;

namespace Game.PatternCombat.CombatEndSystem
{
    public class EndChecker : IDisposable
    {
        private IRegisterUpdate _registerUpdate;

        public Action<SendToOutputData> OnEndCombat; 
        
        public void SubscribeToRegister(IRegisterUpdate unitsRegister)
        {
            _registerUpdate = unitsRegister;
            
            _registerUpdate.SubscribeToUpdate(CheckIsEnd);
        }
        private void CheckIsEnd()
        {
            var playerUnits = _registerUpdate.GetUnits(UnitParent.Player);
            var enemyUnits = _registerUpdate.GetUnits(UnitParent.Enemy);

            if (playerUnits.Count > 0 && enemyUnits.Count > 0)
                return;

            FightResult result = playerUnits.Count > 0 ? FightResult.Win : FightResult.Lose;
            List<UnitWorldInfo> unitList = playerUnits.Values.Select(e => e.GetData().WorldInfo).ToList();

            var outputData = new SendToOutputData
            {
                playerUnits = unitList,
                resultFight = result
            };
            
            OnEndCombat?.Invoke(outputData);
        }
        public void Dispose()
        {
            _registerUpdate.UnsubscribeFromUpdate(CheckIsEnd);
            OnEndCombat = null;
        }
    }
}