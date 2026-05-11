using System.Collections.Generic;
using Game.Data.UnitConfigs;
using UnityEngine;

namespace Game.Data.Player
{
    [CreateAssetMenu(fileName = "playerUnits", menuName = "Config/PlayerUnits", order = 0)]
    public class PlayerUnitCollection : ScriptableObject
    {
        [SerializeField] private List<UnitWorldInfo> startUnits;

        public List<UnitWorldInfo> CloneUnitsConfig()
        {
            var playerUnit = new List<UnitWorldInfo>();

            foreach (var unit in startUnits)
            {
                playerUnit.Add(new UnitWorldInfo
                {
                    unitConfig = unit.unitConfig,
                    unitCount = unit.unitCount
                });
            }

            return playerUnit;
        }
    }
}