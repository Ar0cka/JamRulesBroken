using System;
using System.Collections;
using System.Collections.Generic;
using BattleSystem.UnitSystem.data;
using ScriptableObjects;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace BattleSystem.BattleStategy
{
    public class UnitTakeHit : MonoBehaviour
    {
        [SerializeField] private Animator unitAnimator;
        [SerializeField] private float animationTimeHit;
        [SerializeField] private float animationTimeDead;
        [SerializeField] private TextMeshProUGUI hitPointText;
        [SerializeField] private GameObject textContainer;
        [SerializeField] private Image countBac;
        
        private int _currentHitPoints;
        private int _oneCountHitPoints;

        private int _count;
        private int Count { get => _count; set { _count = value; UpdateHitPointText(); } }

        public void InitializeUnitHitPoints(UnitData unitData, ObjectParent objectParent)
        {
            if (objectParent == ObjectParent.Enemy)
                countBac.color = Color.red;
            
            Count = unitData.Count;
            var unitConfig = unitData.UnitConfig;
            
            _currentHitPoints = unitConfig.Stats.health * Count;
            _oneCountHitPoints = unitConfig.Stats.health;
        }

        private void LateUpdate()
        {
            //hitPointText.transform.LookAt(Camera.current.transform);
            //hitPointText.transform.Rotate(0, 180, 0);
        }

        public IEnumerator TakeHit(UnitConfigs unitConfig, Action unitDeadAction, int damage)
        {
            int finallyDamage = damage;
            
            _currentHitPoints -= finallyDamage;
            
            Count = Mathf.CeilToInt((float)_currentHitPoints / _oneCountHitPoints);

            if (Count <= 0)
            {
                unitAnimator.SetTrigger(unitConfig.Animation.death);
                unitDeadAction?.Invoke();
                yield return new WaitForSeconds(animationTimeDead);
                Destroy(gameObject);
                Debug.Log($"{gameObject.name} is dead :(");
                yield break;
            }
            
            unitAnimator.SetTrigger(unitConfig.Animation.hit);
            yield return new WaitForSeconds(animationTimeHit);
        }

        public void Heal(int unitHeal)
        {
            _currentHitPoints += unitHeal;

            Count = _currentHitPoints <= _oneCountHitPoints ? 1 : Mathf.RoundToInt((float)_currentHitPoints / _oneCountHitPoints);
            
            UpdateHitPointText();
        }
        
        private void UpdateHitPointText()
        {
            hitPointText.text = Count > 0 ? Count.ToString() : "0";
        }
    }
}