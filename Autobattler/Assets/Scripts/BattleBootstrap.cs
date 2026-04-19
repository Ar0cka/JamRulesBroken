using System.Collections;
using System.Collections.Generic;
using BattleSystem;
using Grid;
using UnityEngine;

public class BattleBootstrap : MonoBehaviour
{
    [SerializeField] private GridSystem gridSystem;
    [SerializeField] private TestSpawnerAndMove unitController;
    void Start()
    {
        gridSystem.Initialize();
        unitController.SpawnAndMove();
    }
}
