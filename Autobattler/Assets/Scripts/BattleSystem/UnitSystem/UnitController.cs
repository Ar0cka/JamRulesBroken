using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.BattleStategy;
using BattleSystem.UnitSystem;
using BattleSystem.UnitSystem.data;
using DefaultNamespace.Pathfiender;
using Grid;
using ScriptableObjects;
using UnityEngine;

namespace BattleSystem
{
    public class UnitController : MonoBehaviour
    {
        [Header("Scripts")]
        [SerializeField] private UnitMove unitMove;
        [SerializeField] private UnitAttack unitAttack;
        [SerializeField] private UnitTakeHit unitTakeHit;
        
        [Header("Settings")]
        [SerializeField] private float overlapRadius = 1;
        [SerializeField] private float overlapRadiusArea = 1.5f;
        
        private UnitData _data;
        
        [HideInInspector] public ObjectParent objectParent;

        private Func<string, ObjectParent, bool> _deadFunc;
        private Action _deadAction;
        private Bfs _bfs;

        public int ActionPoints { get; private set; }
        public string UnitName => _data.UnitConfig.UnitName;
        
        public void InitializeUnit(UnitData data, ObjectParent parent, Func<string, ObjectParent, bool> deadFunc, Bfs bfs)
        {
            _data = data;
            _deadFunc = deadFunc;
            objectParent = parent;
            _bfs = bfs;
            
            _deadAction = UnitIsDead;
            unitTakeHit.InitializeUnitHitPoints(data, parent);
        }
        
        public UnitData GetData() => _data;
        public void SetUnitPosition(int x, int y, Vector2 worldPos) => _data.SetPosition(x, y, worldPos);

        public IEnumerator UnitTurnActions(List<GridData> targetPath)
        {
            ActionPoints = _data.UnitConfig.Stats.maxCellsInTurn;

            if (_data.CurrentEffectData.TurnsLess > 0 || _data.CurrentEffectData.EffectType == EffectType.None)
            {
                yield return StartCoroutine(ChooseEffectiveAction());
                _data.CurrentEffectData.EffectTurnPassed();
            }
            
            foreach (var path in targetPath)
            {
                var lastPath = path;
                
                if (ActionPoints <= 0)
                {
                    break;
                }
                
                Vector2 targetPosition = new Vector2(path.WorldX, path.WorldY);
#if UNITY_EDITOR
                _targetPosition = targetPosition;

#endif
                
                var hitAll = Physics2D.OverlapCircleAll(targetPosition, overlapRadius);

                var hit = hitAll.FirstOrDefault(x => x.CompareTag("Unit"));
                
                if (hit != null)
                {
                    var unitController = hit.GetComponent<UnitController>();

                    if (unitController.objectParent != objectParent)
                    {
                        yield return StartCoroutine(Attack(unitController));
                        continue;
                    }
                }
                
                yield return StartCoroutine(Move(targetPosition));
                SetUnitPosition(lastPath.X, lastPath.Y, new Vector2(lastPath.WorldX, lastPath.WorldY));
            }
            
            yield return true;
        }
        
        public IEnumerator Move(Vector2 targetPosition)
        {
            ActionPoints--;
            yield return StartCoroutine(unitMove.Move(targetPosition, _data.UnitConfig.Movement.speed,
                _data.UnitConfig.Animation.walk));
        }
        
        public IEnumerator Attack(UnitController targetController)
        {
            ActionPoints = 0;
            yield return StartCoroutine(unitAttack.Attack(targetController, _data));
        }
        
        public IEnumerator TakeHit(int damage)
        {
            yield return StartCoroutine(unitTakeHit.TakeHit(_data.UnitConfig, _deadAction, damage));
        }

        public void Heal(int healCount)
        {
            unitTakeHit.Heal(healCount);
        }

        public void SetEffective(EffectData effectData, SpellConfig spellConfig)
        {
            if (_data.CurrentEffectData == null)
                _data.SetNewEffectData();

            if (_data.CurrentEffectData != null)
                _data.CurrentEffectData.SetNewEffect(effectData.EffectType, effectData.Turns, spellConfig);
        }

        private IEnumerator ChooseEffectiveAction()
        {
            if (_data.CurrentEffectData.EffectType == EffectType.Cold)
            {
                ActionPoints--;
                yield break;
            }

            if (_data.CurrentEffectData.EffectType == EffectType.Speed)
            {
                ActionPoints++;
                yield break;
            }

            if (_data.CurrentEffectData.EffectType == EffectType.Fire)
            {
                yield return StartCoroutine(unitTakeHit.TakeHit(_data.UnitConfig, _deadAction,
                    _data.CurrentEffectData.CurrentSpellData.SpellStats.spellDamage));
            }
        }
        
        private void UnitIsDead()
        {
            var isDeleted = _deadFunc.Invoke(_data.UnitConfig.UnitName, objectParent);
            
            if (isDeleted)
            {
                Debug.Log("Destroying unit after dead");
                Destroy(gameObject);
            }
        }

#if UNITY_EDITOR
        private Vector2 _targetPosition;
        
        private void OnDrawGizmos()
        {
            if (_targetPosition == Vector2.zero)
                return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_targetPosition, overlapRadius);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, overlapRadiusArea);
        }
#endif
    }
}