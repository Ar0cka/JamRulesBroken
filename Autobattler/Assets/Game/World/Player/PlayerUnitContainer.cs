using System;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.UnitSystem.data;
using Game.Data;
using Game.Data.Player;
using Game.Data.UnitConfigs;
using Game.World.Player.Interfaces;
using UISystem;
using UnityEngine;

namespace Game.World.Player
{
    public class PlayerUnitContainer : MonoBehaviour, IPlayerContainer
    {
        [SerializeField] private PlayerUnitCollection startUnits;
        [SerializeField] private List<HydeUnits> unitsImage;
        
        private readonly Dictionary<string, UnitWorldInfo> _playerUnits = new();
        public Dictionary<string, UnitWorldInfo> PlayerUnits => _playerUnits;

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

        public void AddUnit(UnitWorldInfo unitWorldInfo)
        {
            if (_playerUnits.TryGetValue(unitWorldInfo.unitConfig.UnitID, out var value))
            {
                value.unitCount += unitWorldInfo.unitCount;
                UpdateImages();
                return;
            }
            
            _playerUnits.Add(unitWorldInfo.unitConfig.UnitID, unitWorldInfo);
            UpdateImages();
        }
        public void RebuildUnitsAfterFight(List<UnitBattleStates> unitData)
        {
            _playerUnits.Clear();

            foreach (var data in unitData)
            {
                _playerUnits.Add(data.UnitConfig.UnitID, new UnitWorldInfo
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
}