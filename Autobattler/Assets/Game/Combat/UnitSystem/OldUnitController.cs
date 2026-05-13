using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.BattleStategy;
using BattleSystem.UnitSystem.data;
using Cysharp.Threading.Tasks;
using Game.Data.SpellConfigs;
using Grid;
using UnityEngine;

namespace BattleSystem
{
    public class OldUnitController : MonoBehaviour
    {
        [Header("Scripts")]
        [SerializeField] private UnitMove unitMove;
        [SerializeField] private UnitAttack unitAttack;
        [SerializeField] private UnitTakeHit unitTakeHit;
        
        [Header("Settings")]
        [SerializeField] private float overlapRadius = 1;
        [SerializeField] private float overlapRadiusArea = 1.5f;
        [SerializeField] private float distance = 2f;
        private UnitCombatInfo _combatInfo;
        
        [HideInInspector] public UnitParent unitParent;

        private Func<string, UnitParent, bool> _deadFunc;
        private Action _deadAction;
        private GridSystem _gridSystem;

        public int ActionPoints { get; private set; }
        public string UnitName => _combatInfo.WorldInfo.unitConfig.UnitDefinition.unitName;
        
        public void InitializeUnit(UnitCombatInfo combatInfo, UnitParent parent, GridSystem gridSystem)
        {
            _combatInfo = combatInfo;
            unitParent = parent;
            _gridSystem = gridSystem;
          
            
            _deadAction = UnitIsDead;
            unitTakeHit.InitializeUnitHitPoints(combatInfo, parent);
        }
        
        public UnitCombatInfo GetData() => _combatInfo;
        public void SetUnitPosition(int x, int y, Vector2 worldPos)
        {
            if (_combatInfo.X != -1 && _combatInfo.Y != -1) 
                _gridSystem.SetWalkable(_combatInfo.X, _combatInfo.Y, true);
            
            _combatInfo.SetPosition(x, y, worldPos);
            _gridSystem.SetWalkable(x, y, false);
        }

        public async UniTask UnitAction()
        {
            
        }

        public void ChooseTarget()
        {
            
        }
        
        public IEnumerator UnitTurnActions(List<GridData> targetPath)
        {
            ActionPoints = _combatInfo.WorldInfo.unitConfig.Stats.cellsInTurn;

            if (_combatInfo.CurrentEffectData.TurnsLess > 0 || _combatInfo.CurrentEffectData.EffectType == EffectType.None)
            {
                yield return StartCoroutine(ChooseEffectiveAction());
                _combatInfo.CurrentEffectData.EffectTurnPassed();
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
                    var unitController = hit.GetComponent<OldUnitController>();

                    if (unitController.unitParent != unitParent)
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
            yield return StartCoroutine(unitMove.Move(targetPosition, _combatInfo.WorldInfo.unitConfig.Movement.speed,
                _combatInfo.WorldInfo.unitConfig.Animation.walk));
        }
        
        public IEnumerator Attack(OldUnitController targetController)
        {
            ActionPoints = 0;
            yield return StartCoroutine(unitAttack.Attack(targetController, _combatInfo));
        }
        
        public IEnumerator TakeHit(int damage)
        {
            yield return StartCoroutine(unitTakeHit.TakeHit(_combatInfo.WorldInfo.unitConfig, _deadAction, damage));
        }

        public void Heal(int healCount)
        {
            unitTakeHit.Heal(healCount);
        }

        public void SetEffective(EffectData effectData, SpellConfig spellConfig)
        {
            if (_combatInfo.CurrentEffectData == null)
                _combatInfo.SetNewEffectData();

            if (_combatInfo.CurrentEffectData != null)
                _combatInfo.CurrentEffectData.SetNewEffect(effectData.EffectType, effectData.Turns, spellConfig);
        }

        private IEnumerator ChooseEffectiveAction()
        {
            if (_combatInfo.CurrentEffectData.EffectType == EffectType.Cold)
            {
                ActionPoints--;
                yield break;
            }

            if (_combatInfo.CurrentEffectData.EffectType == EffectType.Speed)
            {
                ActionPoints++;
                yield break;
            }

            if (_combatInfo.CurrentEffectData.EffectType == EffectType.Fire)
            {
                yield return StartCoroutine(unitTakeHit.TakeHit(_combatInfo.WorldInfo.unitConfig, _deadAction,
                    _combatInfo.CurrentEffectData.CurrentSpellData.SpellStats.spellDamage));
            }
        }
        
        private void UnitIsDead()
        {
            var delted = _deadFunc.Invoke(_combatInfo.WorldInfo.unitConfig.UnitDefinition.unitName, unitParent);
            
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