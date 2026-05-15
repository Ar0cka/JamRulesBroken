using System;
using System.Collections.Generic;
using Game.Core.BaseUnits;
using UnityEngine;

namespace Game.PatternCombat.BattleUnitSystem
{
    public class UnitsRegister : IRegisterUpdate, IUnitRegister
    {
        private Dictionary<string, BaseUnitController> _playerUnits = new();
        private Dictionary<string, BaseUnitController> _enemyUnits = new();

        private Action _onRegisterUpdate;
        
        public void AddUnit(UnitParent parent, BaseUnitController oldUnit)
        {
            var dictionary = parent == UnitParent.Player ? _playerUnits : _enemyUnits;
            var unitInfo = oldUnit.GetUnitInfo().UnitInfo.unitConfig;

            dictionary[unitInfo.UnitID] = oldUnit;
            
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
        
        public Dictionary<string, BaseUnitController> GetUnits(UnitParent parent)
        {
            var dictionary = parent == UnitParent.Player ? _playerUnits : _enemyUnits;

            return dictionary;
        }
    }

    public interface IRegisterUpdate
    {
        public void SubscribeToUpdate(Action method);
        public void UnsubscribeFromUpdate(Action method);
        public Dictionary<string, BaseUnitController> GetUnits(UnitParent parent);
    }

    public interface IUnitRegister
    {
        public void AddUnit(UnitParent parent, BaseUnitController oldUnit);
        public void RemoveUnit(UnitParent parent, string unitId);
        public Dictionary<string, BaseUnitController> GetUnits(UnitParent parent);
    }
}