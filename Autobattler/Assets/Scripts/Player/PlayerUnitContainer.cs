using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using TMPro;
using UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerUnitContainer : MonoBehaviour
    {
        [SerializeField] private PlayerUnitCollection startUnits;
        [SerializeField] private List<HydeUnits> unitsImage;
        
        private readonly Dictionary<string, PlayerUnit> _playerUnits = new();

        private void Awake()
        {
            var units = startUnits.CloneUnitsConfig();

            foreach (var unit in units)
            {
                _playerUnits.Add(unit.unitConfig.UnitName, unit);
            }

            foreach (var image in unitsImage)
            {
                image.gameObject.SetActive(false);
            }
            
            UpdateImages();
        }

        public void AddUnit(PlayerUnit unit)
        {
            if (_playerUnits.TryGetValue(unit.unitConfig.UnitName, out var value))
            {
                value.unitCount += unit.unitCount;
                UpdateImages();
                return;
            }
            
            _playerUnits.Add(unit.unitConfig.UnitName, unit);
            UpdateImages();
        }

        public void RemoveUnits(UnitDeathData data)
        {
            string deletedUnitName = "";
            
            if (_playerUnits.TryGetValue(data.unitName, out var value))
            {
                value.unitCount -= data.deathCount;

                if (value.unitCount <= 0)
                {
                    deletedUnitName = value.unitConfig.UnitName;
                    value.unitCount = 0;
                }
            }
            
            UpdateImages();

            if (!string.IsNullOrEmpty(deletedUnitName))
                _playerUnits[deletedUnitName] = null;
        }

        private void UpdateImages()
        {
            for (int i = 0; i < unitsImage.Count; i++)
            {
                if (i >= _playerUnits.Count)
                {
                    break;
                }
                
                var value = _playerUnits.ElementAt(i).Value;
                if (value.unitCount == 0)
                {
                    unitsImage[i].gameObject.SetActive(false);
                    continue;
                }
                
                if (!unitsImage[i].gameObject.activeInHierarchy)
                    unitsImage[i].gameObject.SetActive(true);
                
                unitsImage[i].SetUnit(value.unitConfig.UnitSprite, value.unitCount);
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