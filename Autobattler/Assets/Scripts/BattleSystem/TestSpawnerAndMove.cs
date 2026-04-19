using System;
using System.Collections;
using Grid;
using ScriptableObjects;
using UnityEngine;

namespace BattleSystem
{
    public class TestSpawnerAndMove : MonoBehaviour
    {
        [SerializeField] private GridSystem gridSystem;
        [SerializeField] private UnitConfigs testConfig;

        private UnitController _unitController;
        private Coroutine _currentDeathCoroutine;
        
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _currentDeathCoroutine == null)
            {
                _currentDeathCoroutine = StartCoroutine(_unitController.Death());
            }
        }

        public void SpawnAndMove()
        {
            var spawnPosition = gridSystem.GetPosition(1, 0);
            
            Debug.Log($"Spawn position {spawnPosition}");
            
            var unit = Instantiate(testConfig.UnitModel);
            unit.transform.position = spawnPosition;

            _unitController = unit.GetComponent<UnitController>();
            
            if (_unitController == null)
            {
                Debug.LogError("Unit controller is not founded");
                return;
            }
            
            StartCoroutine(StartMove(_unitController, spawnPosition));
        }
        
        private IEnumerator StartMove(UnitController unitController, Vector2 startPosition)
        {
            yield return StartCoroutine(unitController.Move(gridSystem.GetPosition(1,1)));
            yield return StartCoroutine(unitController.Move(gridSystem.GetPosition(3,3)));
            yield return StartCoroutine(unitController.Move(startPosition));
        }
    }
}