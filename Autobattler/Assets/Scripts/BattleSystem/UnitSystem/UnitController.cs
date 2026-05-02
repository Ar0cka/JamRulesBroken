using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.BattleStategy;
using BattleSystem.UnitSystem.data;
using Grid;
using ScriptableObjects;
using ScriptableObjects.SpellConfigs;
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
        [SerializeField] private float distance = 2f;
        private UnitBattleStates _battleStates;
        
        [HideInInspector] public ObjectParent objectParent;

        private Func<string, ObjectParent, bool> _deadFunc;
        private Action _deadAction;
        private GridSystem _gridSystem;

        public int ActionPoints { get; private set; }
        public string UnitName => _battleStates.UnitConfig.UnitName;
        
        public void InitializeUnit(UnitBattleStates battleStates, ObjectParent parent, Func<string, ObjectParent, bool> deadFunc, GridSystem gridSystem)
        {
            _battleStates = battleStates;
            _deadFunc = deadFunc;
            objectParent = parent;
            _gridSystem = gridSystem;
          
            
            _deadAction = UnitIsDead;
            unitTakeHit.InitializeUnitHitPoints(battleStates, parent);
        }
        
        public UnitBattleStates GetData() => _battleStates;
        public void SetUnitPosition(int x, int y, Vector2 worldPos)
        {
            if (_battleStates.X != -1 && _battleStates.Y != -1) 
                _gridSystem.SetWalkable(_battleStates.X, _battleStates.Y, true);
            
            _battleStates.SetPosition(x, y, worldPos);
            _gridSystem.SetWalkable(x, y, false);
        }

        public IEnumerator UnitTurnActions(List<GridData> targetPath)
        {
            ActionPoints = _battleStates.UnitConfig.Stats.cellsInTurn;

            if (_battleStates.CurrentEffectData.TurnsLess > 0 || _battleStates.CurrentEffectData.EffectType == EffectType.None)
            {
                yield return StartCoroutine(ChooseEffectiveAction());
                _battleStates.CurrentEffectData.EffectTurnPassed();
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
            yield return StartCoroutine(unitMove.Move(targetPosition, _battleStates.UnitConfig.Movement.speed,
                _battleStates.UnitConfig.Animation.walk));
        }
        
        public IEnumerator Attack(UnitController targetController)
        {
            ActionPoints = 0;
            yield return StartCoroutine(unitAttack.Attack(targetController, _battleStates));
        }
        
        public IEnumerator TakeHit(int damage)
        {
            yield return StartCoroutine(unitTakeHit.TakeHit(_battleStates.UnitConfig, _deadAction, damage));
        }

        public void Heal(int healCount)
        {
            unitTakeHit.Heal(healCount);
        }

        public void SetEffective(EffectData effectData, SpellConfig spellConfig)
        {
            if (_battleStates.CurrentEffectData == null)
                _battleStates.SetNewEffectData();

            if (_battleStates.CurrentEffectData != null)
                _battleStates.CurrentEffectData.SetNewEffect(effectData.EffectType, effectData.Turns, spellConfig);
        }

        private IEnumerator ChooseEffectiveAction()
        {
            if (_battleStates.CurrentEffectData.EffectType == EffectType.Cold)
            {
                ActionPoints--;
                yield break;
            }

            if (_battleStates.CurrentEffectData.EffectType == EffectType.Speed)
            {
                ActionPoints++;
                yield break;
            }

            if (_battleStates.CurrentEffectData.EffectType == EffectType.Fire)
            {
                yield return StartCoroutine(unitTakeHit.TakeHit(_battleStates.UnitConfig, _deadAction,
                    _battleStates.CurrentEffectData.CurrentSpellData.SpellData.spellDamage));
            }
        }
        
        private void UnitIsDead()
        {
            var delted = _deadFunc.Invoke(_battleStates.UnitConfig.UnitName, objectParent);
            
            if (delted)
                Destroy(gameObject);
            
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