using System;
using System.Collections.Generic;
using ScriptableObjects.UnitConfigs;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "playerUnits", menuName = "Config/PlayerUnits", order = 0)]
    public class PlayerUnitCollection : ScriptableObject
    {
        [SerializeField] private List<PlayerUnit> startUnits;

        public List<PlayerUnit> CloneUnitsConfig()
        {
            var playerUnit = new List<PlayerUnit>();

            foreach (var unit in startUnits)
            {
                playerUnit.Add(new PlayerUnit
                {
                    unitConfig = unit.unitConfig,
                    unitCount = unit.unitCount
                });
            }

            return playerUnit;
        }
    }

    [Serializable]
    public class PlayerUnit
    {
        public UnitConfig unitConfig;
        public int unitCount;
    }
}