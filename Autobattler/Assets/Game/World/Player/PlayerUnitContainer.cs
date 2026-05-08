using System;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.UnitSystem.data;
using Game.Data;
using Game.Data.Player;
using Game.World.Player.Interfaces;
using UISystem;
using UnityEngine;

namespace Game.World.Player
{
    public class PlayerUnitContainer : MonoBehaviour, IPlayerContainer
    {
        [SerializeField] private PlayerUnitCollection startUnits;
        [SerializeField] private List<HydeUnits> unitsImage;
        
        private readonly Dictionary<string, PlayerUnit> _playerUnits = new();
        public Dictionary<string, PlayerUnit> PlayerUnits => _playerUnits;

        private void Awake()
        {
            var units = startUnits.CloneUnitsConfig();

            foreach (var unit in units)
            {
                _playerUnits.Add(unit.unitConfig.UnitID, unit);
            }

            foreach (var image in unitsImage)
            {
                image.gameObject.SetActive(false);
            }
            
            UpdateImages();
        }

        public void AddUnit(PlayerUnit unit)
        {
            if (_playerUnits.TryGetValue(unit.unitConfig.UnitID, out var value))
            {
                value.unitCount += unit.unitCount;
                UpdateImages();
                return;
            }
            
            _playerUnits.Add(unit.unitConfig.UnitID, unit);
            UpdateImages();
        }
        public void RebuildUnitsAfterFight(List<UnitBattleStates> unitData)
        {
            _playerUnits.Clear();

            foreach (var data in unitData)
            {
                _playerUnits.Add(data.UnitConfig.UnitID, new PlayerUnit
                {
                    unitConfig = data.UnitConfig,
                    unitCount = data.Count
                });
            }
            
            UpdateImages();
        }

        private void UpdateImages()
        {
            foreach (var image in unitsImage)
            {
                image.gameObject.SetActive(false);
            }
            
            for (int i = 0; i < unitsImage.Count; i++)
            {
                if (i >= _playerUnits.Count)
                {
                    break;
                }
                
                var value = _playerUnits.ElementAt(i).Value;
                
                if (!unitsImage[i].gameObject.activeInHierarchy)
                    unitsImage[i].gameObject.SetActive(true);
                
                unitsImage[i].SetUnit(value.unitConfig.VisualData.unitSprite, value.unitCount);
            }
        }
    }

    [Serializable]
    public class UnitDeathData
    {
        public string unitName;
        public int deathCount;
    }
}