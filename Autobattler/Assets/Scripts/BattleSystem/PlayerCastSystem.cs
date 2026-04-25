using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using TMPro;
using UISystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BattleSystem
{
    public class PlayerCastSystem : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Transform spellPosition;
        [SerializeField] private TextMeshProUGUI spellNameBox;
        [SerializeField] private ErrorMessage errorMessage;
        [SerializeField] private TurnController turnController;
        private Dictionary<string, SpellConfig> _playerSpellList = new();

        private string _chooseSpell = "";
        private bool isCast = false;
        public bool IsCast => isCast;
        public bool IsCanCast { get; private set; }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject() || _playerSpellList.Count == 0 || turnController.IsTurn)
                return;

            if (!string.IsNullOrEmpty(_chooseSpell) && Input.GetMouseButtonDown(0) && !isCast && IsCanCast)
            {
                isCast = true;
                
                var currentMousePos = mainCamera.ScreenPointToRay(Input.mousePosition);
                
                var hit = Physics2D.RaycastAll(currentMousePos.origin, currentMousePos.direction);
                
                var unit = hit.FirstOrDefault(x => x.collider.CompareTag("Unit"));
                
                if (unit.collider == null)
                {
                    errorMessage.OpenPanel(ErrorType.SpellType, "You put not unit target");
                    UnsetCastInBeginTurn();
                    return;
                }
                
                var unitController = unit.collider.GetComponent<UnitController>();

                if (unitController is not null)
                {
                    StartCoroutine(CastSpell(_chooseSpell, unitController));
                }
                else
                {
                    errorMessage.OpenPanel(ErrorType.SpellType, "You put not unit target");
                    UnsetCastInBeginTurn();
                }
            } 
        }

        public void InitializeSpells(List<SpellConfig> spellConfigs)
        {
            _playerSpellList = spellConfigs.ToDictionary(x => x.SpellName, x => x);
        }
        
        private IEnumerator CastSpell(string spellName, UnitController unitController)
        {
            if (_playerSpellList.TryGetValue(spellName, out var value))
            {
                var spellObject = Instantiate(value.SpellVfx);
                spellObject.transform.position = new Vector3(spellPosition.position.x, spellPosition.position.y, 0);

                Debug.Log($"Hit unit: {unitController.name} and him position = {unitController.GetData().CurrentWorldPosition}");
                yield return StartCoroutine(spellObject.GetComponent<SpellProjectail>().Action(unitController, value, unitController.GetData().CurrentWorldPosition));

                _chooseSpell = "";
                spellNameBox.gameObject.SetActive(false);
                IsCanCast = false;
            }
        }

        public void UnsetCastInBeginTurn()
        {
            isCast = false;
            IsCanCast = true;
        }

        public void SetSpellName(string nameSpell)
        {
            _chooseSpell = nameSpell;
            spellNameBox.gameObject.SetActive(true);
            spellNameBox.text = $"Current spell: {nameSpell}";
        }
    }
}