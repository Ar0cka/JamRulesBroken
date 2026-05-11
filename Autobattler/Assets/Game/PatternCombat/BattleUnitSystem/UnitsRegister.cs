using System;
using System.Collections.Generic;
using BattleSystem;
using UnityEngine;

namespace Game.PatternCombat.BattleUnitSystem
{
    public class UnitsRegister : IRegisterUpdate
    {
        private Dictionary<string, UnitController> _playerUnits = new();
        private Dictionary<string, UnitController> _enemyUnits = new();

        private Action _onRegisterUpdate;
        
        public void AddUnit(UnitParent parent, UnitController unit)
        {
            var dictionary = parent == UnitParent.Player ? _playerUnits : _enemyUnits;
            var unitInfo = unit.GetData().WorldInfo.unitConfig;

            dictionary[unitInfo.UnitID] = unit;
            
            _onRegisterUpdate?.Invoke();
        }
        
        public void RemoveUnit(UnitParent parent, string unitId)
        {
            var dictionary = parent == UnitParent.Player ? _playerUnits : _enemyUnits;

            if (!dictionary.Remove(unitId))
            {
                Debug.Log("Not find target unit");
                return;
            }
            
            _onRegisterUpdate?.Invoke();
        }

        public void SubscribeToUpdate(Action method)
        {
            _onRegisterUpdate += method;
        }
        public void UnsubscribeFromUpdate(Action method)
        {
            _onRegisterUpdate -= method;
        }
        
        public Dictionary<string, UnitController> GetUnits(UnitParent parent)
        {
            var dictionary = parent == UnitParent.Player ? _playerUnits : _enemyUnits;

            return dictionary;
        }
    }

    public interface IRegisterUpdate
    {
        public void SubscribeToUpdate(Action method);
        public void UnsubscribeFromUpdate(Action method);
        public Dictionary<string, UnitController> GetUnits(UnitParent parent);
    }
}