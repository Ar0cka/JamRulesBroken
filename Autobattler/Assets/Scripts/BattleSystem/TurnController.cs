using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.UnitSystem;
using BattleSystem.UnitSystem.data;
using DefaultNamespace.Pathfiender;
using Grid;
using ScriptableObjects;
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
        
        private Func<string, ObjectParent, bool> _isUnitDead;
        private Action<SendToOutputData> _isOutputData;
        private Action _nextUnitTurn;

        public Action WaitActionPlayer;

        private int _currentTurn;

        private bool _isEnd;
        private bool _isTurn;
        private bool _isInitialized = false;
        
        public void Initialize(SendToBattleData data, Action<SendToOutputData> outputDataEvent)
        {
            _isUnitDead = UnitDead;
            _isOutputData = outputDataEvent;
         
            foreach (var unit in data.PlayerUnits)
            { 
                CreateUnit(ObjectParent.Player, unit);
            }
            foreach (var enemyUnit in data.EnemyUnits)
            {
                CreateUnit(ObjectParent.Enemy, enemyUnit);
            }
            
            WaitActionPlayer += () =>
            {
                if (_isTurn)
                    return;

                _isTurn = true;
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
            
            endPlayerTurnButton.interactable = !_isTurn;
        }

        private IEnumerator StartUnitsActions()
        {
            while (_turnQueue.Count > 0)
            {
                var unit = _turnQueue.Dequeue();
                
                if (unit == null || !IsHaveInDictionary(unit.UnitName))
                {
                    continue;
                }

                if (unit.GetData().UnitConfig.Type == UnitType.Range)
                {
                    var hit = Physics2D.OverlapCircleAll(unit.transform.position, overlapRadius);

#if UNITY_EDITOR
                    overlapVector = unit.transform.position;
#endif

                    if (hit.Any(h => h.CompareTag("Unit") && h.GetComponent<UnitController>().objectParent != unit.objectParent))
                    {
                        var freeSell = gridSystem.GetFreeCells(unit.GetData().X, unit.GetData().Y, 0.5f);
                        
                        if (freeSell != null)
                        {
                            var targetVector = new Vector2(freeSell.WorldX, freeSell.WorldY);
                            
                            yield return unit.Move(targetVector);
                            unit.SetUnitPosition(freeSell.X, freeSell.Y, targetVector);
                        }
                        
                        var hitFirst = hit.FirstOrDefault(x => x.CompareTag("Unit"));
                        
                        if (hitFirst != null)
                        {
                            var unitControllerUnitRange = hitFirst.GetComponent<UnitController>();
                            yield return StartCoroutine(unit.Attack(unitControllerUnitRange));
                            continue;
                        }
                    }
                    
                    var unitTarget = GetRandomEnemyUnit(unit);
                    yield return StartCoroutine(unit.Attack(unitTarget));
                    continue;
                }
                
                var targetUnit = FindTarget(unit.objectParent, unit.transform);

                var data = targetUnit.GetData();
                
                var path = pathfinder.CalculatePath(unit.GetData().X, unit.GetData().Y, data.X, data.Y);

                yield return unit.UnitTurnActions(path);
            }
            
            CreateNextTurn();
        }

        private UnitController FindTarget(ObjectParent currentUnitType, Transform currentPos)
        {
            var hit = Physics2D.OverlapAreaAll(currentPos.position,
                currentPos.position + new Vector3(overlapRadius, overlapRadius, 0));

            foreach (var target in hit)
            {
                if (!target.CompareTag("Unit"))
                    continue;
                
                var unitController = target.GetComponent<UnitController>();
                
                if (unitController.objectParent == currentUnitType) continue;

                return unitController;
            }

            switch (currentUnitType)
            {
                case ObjectParent.Player:
                    return _enemyUnits.Values.ElementAt(Random.Range(0, _enemyUnits.Count - 1));
                case ObjectParent.Enemy:
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
            switch (currentUnit.objectParent)
            {
                case ObjectParent.Player:
                    return _enemyUnits.Values.ElementAt(Random.Range(0, _enemyUnits.Count));
                default:
                    return _playerUnits.Values.ElementAt(Random.Range(0, _playerUnits.Count));
            }
        }
        
        private void CreateUnit(ObjectParent parent, UnitData unit)
        {
            var spawnPoint = parent == ObjectParent.Player ? gridSystem.GetRandomPlayerCell() : gridSystem.GetRandomEnemyCell();
                
            unit.SetPosition(spawnPoint.x, spawnPoint.y, gridSystem.GetPosition(spawnPoint.y, spawnPoint.x));

            var unitObject = Instantiate(unit.UnitConfig.UnitModel);
            unitObject.transform.position = unit.CurrentWorldPosition;
            unitObject.name = $"{unitObject.name}:{parent}";
            
            var unitController = unitObject.GetComponent<UnitController>();
            unitController.InitializeUnit(unit, parent, _isUnitDead, pathfinder);

            if (unitController.objectParent == ObjectParent.Enemy)
            {
                unitController.GetComponent<SpriteRenderer>().flipX = true;
            }
            
            if (parent == ObjectParent.Player)
            {
                _playerUnits.Add(unit.UnitConfig.UnitName, unitController);
            }
            else
                _enemyUnits.Add(unit.UnitConfig.UnitName, unitController);
        }
        private void CreateNextTurn()
        {
            _currentTurn++;
            
            _isTurn = false;
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
        private bool UnitDead(string unitName, ObjectParent parent)
        {
            try
            {
                switch (parent)
                {
                    case ObjectParent.Player:
                        _playerUnits.Remove(unitName);
                        break;
                    case ObjectParent.Enemy:
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
            outputData.UnitData = new();
            
            foreach (var unit in _playerUnits.Values)
            {
                outputData.UnitData.Add(unit.GetData());
            }

            outputData.ResultFight = _playerUnits.Count > 0 ? FightResult.Win : FightResult.Lose;
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
    
    public enum ObjectParent
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