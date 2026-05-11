using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.UnitSystem;
using BattleSystem.UnitSystem.data;
using DefaultNamespace.Pathfiender;
using Game.Combat.UnitSystem.data;
using Game.Data.UnitConfigs;
using Grid;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace BattleSystem
{
    public class TurnController : MonoBehaviour, IDisposable
    {
        [Header("Systems")]
        [SerializeField] private GridSystem gridSystem;
        [SerializeField] private Bfs pathfinder;
        
        [Header("UI")]
        [SerializeField] private TextMeshProUGUI turnText;
        [SerializeField] private Button endPlayerTurnButton;
        
        [Header("Settings")] 
        [SerializeField] private int overlapRadius;

        [SerializeField] private PlayerCastSystem playerCast;

        private Dictionary<string, UnitController> _playerUnits = new();
        private Dictionary<string, UnitController> _enemyUnits = new();
        
        private Queue<UnitController> _turnQueue = new();
        
        private Func<string, UnitParent, bool> _isUnitDead;
        private Action<SendToOutputData> _isOutputData;
        private Action _nextUnitTurn;

        public Action WaitActionPlayer;

        private int _currentTurn;

        private bool _isEnd;
        public bool IsTurn { get; private set; }
        private bool _isInitialized = false;

        private UnitController _currentUnit;
        private Coroutine _unitCoroutine;
        
        public void Initialize(SendToBattleData data, Action<SendToOutputData> outputDataEvent)
        {
            _isUnitDead = UnitDead;
            _isOutputData = outputDataEvent;
         
            foreach (var unit in data.playerUnits)
            { 
                CreateUnit(UnitParent.Player, unit);
            }
            foreach (var enemyUnit in data.enemyUnits)
            {
                CreateUnit(UnitParent.Enemy, enemyUnit);
            }
            
            WaitActionPlayer += () =>
            {
                if (IsTurn)
                    return;

                IsTurn = true;
                StartCoroutine(StartUnitsActions());
            };
            
            endPlayerTurnButton.onClick.AddListener(() => WaitActionPlayer?.Invoke());

            CreateNextTurn();
            _isInitialized = true;
        }

        private void Update()
        {
            if (!_isInitialized) return;
            
            if (_playerUnits.Count == 0 || _enemyUnits.Count == 0 && !_isEnd)
            {
                _isEnd = true;
                EndBattle();
            }
            
            endPlayerTurnButton.interactable = !IsTurn;
        }

        private IEnumerator StartUnitsActions()
        {
            while (_turnQueue.Count > 0)
            {
                _currentUnit = _turnQueue.Dequeue();
                
                if (_currentUnit == null || !IsHaveInDictionary(_currentUnit.UnitName))
                {
                    continue;
                }

                if (_currentUnit.GetData().UnitConfig.UnitDefinition.unitType == UnitType.Range)
                {
                    var hit = Physics2D.OverlapCircleAll(_currentUnit.transform.position, overlapRadius);
#if UNITY_EDITOR
                    overlapVector = _currentUnit.transform.position;
#endif

                    if (hit.Any(h => h.CompareTag("Unit") && h.GetComponent<UnitController>().unitParent != _currentUnit.unitParent))
                    {
                        var freeSell = gridSystem.GetFreeCells(_currentUnit.GetData().X, _currentUnit.GetData().Y, 0.5f);
                        
                        if (freeSell != null)
                        {
                            if (_currentUnit == null || !IsHaveInDictionary(_currentUnit.UnitName)) 
                                continue;
                            
                            var targetVector = new Vector2(freeSell.WorldX, freeSell.WorldY);
                            
                            yield return _currentUnit.Move(targetVector);
                            _currentUnit.SetUnitPosition(freeSell.X, freeSell.Y, targetVector);
                        }
                        
                        var hitFirst = hit.FirstOrDefault(x => x.CompareTag("Unit") && x.GetComponent<UnitController>().unitParent != _currentUnit.unitParent);
                        
                        if (hitFirst != null)
                        {
                            if (_currentUnit == null || !IsHaveInDictionary(_currentUnit.UnitName)) 
                                continue;
                            
                            var unitControllerUnitRange = hitFirst.GetComponent<UnitController>();
                            yield return StartCoroutine(_currentUnit.Attack(unitControllerUnitRange));
                            continue;
                        }
                    }
                    if (_currentUnit == null || !IsHaveInDictionary(_currentUnit.UnitName))
                        continue;
                    
                    var unitTarget = GetRandomEnemyUnit(_currentUnit);
                    yield return StartCoroutine(_currentUnit.Attack(unitTarget));
                    yield return new WaitForSeconds(0.5f);
                    continue;
                }
                
                var targetUnit = FindTarget(_currentUnit.unitParent, _currentUnit.transform);

                var data = targetUnit.GetData();
                
                var path = pathfinder.CalculatePath(_currentUnit.GetData().X, _currentUnit.GetData().Y, data.X, data.Y);

                yield return _currentUnit.UnitTurnActions(path);
                yield return new WaitForSeconds(0.5f);
            }
            
            CreateNextTurn();
        }

        private UnitController FindTarget(UnitParent currentUnitType, Transform currentPos)
        {
            var hit = Physics2D.OverlapAreaAll(currentPos.position,
                currentPos.position + new Vector3(overlapRadius, overlapRadius, 0));

            foreach (var target in hit)
            {
                if (!target.CompareTag("Unit"))
                    continue;
                
                var unitController = target.GetComponent<UnitController>();
                
                if (unitController.unitParent == currentUnitType) continue;

                return unitController;
            }

            switch (currentUnitType)
            {
                case UnitParent.Player:
                    return _enemyUnits.Values.ElementAt(Random.Range(0, _enemyUnits.Count - 1));
                case UnitParent.Enemy:
                    return _playerUnits.Values.ElementAt(Random.Range(0, _playerUnits.Count - 1));
            }

            return null;
        }

        private bool IsHaveInDictionary(string unitName)
        {
            return _playerUnits.ContainsKey(unitName) || _enemyUnits.ContainsKey(unitName);
        }

        private UnitController GetRandomEnemyUnit(UnitController currentUnit)
        {
            switch (currentUnit.unitParent)
            {
                case UnitParent.Player:
                    return _enemyUnits.Values.ElementAt(Random.Range(0, _enemyUnits.Count));
                default:
                    return _playerUnits.Values.ElementAt(Random.Range(0, _playerUnits.Count));
            }
        }
        
        private void CreateUnit(UnitParent parent, UnitBattleStates unit)
        {
           
        }
        private void CreateNextTurn()
        {
            _currentTurn++;
            
            IsTurn = false;
            playerCast.UnsetCastInBeginTurn();
            
            UpdateUI();
            
            SortedTurnQueue();
        }
        private void SortedTurnQueue()
        {
            var list = new List<UnitController>();
            
            list.AddRange(_playerUnits.Values);
            list.AddRange(_enemyUnits.Values);

            list.Sort((a, b) => b.GetData().UnitConfig.Stats.initiative.CompareTo(a.GetData().UnitConfig.Stats.initiative));
            
            foreach (var unit in list)
            {
                _turnQueue.Enqueue(unit);
            }
        }
        private void UpdateUI()
        {
            turnText.text = $"Turn: {_currentTurn}";
        }
        private bool UnitDead(string unitName, UnitParent parent)
        {
            try
            {
                switch (parent)
                {
                    case UnitParent.Player:
                        _playerUnits.Remove(unitName);
                        break;
                    case UnitParent.Enemy:
                        _enemyUnits.Remove(unitName);
                        break;
                }

                return true;
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return false;
            }
        }
        private void EndBattle()
        {
            var outputData = new SendToOutputData();
            outputData.playerUnits = new();
            
            foreach (var unit in _playerUnits.Values)
            {
                outputData.playerUnits.Add(unit.GetData());
            }

            outputData.resultFight = _playerUnits.Count > 0 ? FightResult.Win : FightResult.Lose;
            _isOutputData.Invoke(outputData);
            
            //TODO Switch scene
            
            Dispose();
        }

        public void Dispose()
        {
            _isUnitDead -= UnitDead;
            _isUnitDead = null;

            WaitActionPlayer -= () => StartCoroutine(StartUnitsActions());
            WaitActionPlayer = null;

            _isOutputData = null;
            _nextUnitTurn = null;
            
            _turnQueue.Clear();
            _playerUnits.Clear();
            _enemyUnits.Clear();
            
            _isInitialized = false;
        }

#if UNITY_EDITOR
        private Vector2 overlapVector;
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(overlapVector, overlapRadius);
        }
#endif
    }
    
    public enum UnitParent
    {
        Player,
        Enemy
    }

    public enum FightResult
    {
        Win,
        Lose
    }
}