using System;
using System.Collections;
using System.Collections.Generic;
using BattleSystem;
using BattleSystem.UnitSystem;
using BattleSystem.UnitSystem.data;
using Grid;
using ScriptableObjects;
using UnityEngine;

public class BattleBootstrap : MonoBehaviour
{
    [SerializeField] private GridSystem gridSystem;
    [SerializeField] private TurnController turnManager;
    [SerializeField] private List<UnitData> unitsPlayer;
    [SerializeField] private List<UnitData> unitsEnemy;
    [SerializeField] private List<SpellConfig> spellsConfigs;
    [SerializeField] private PlayerCastSystem playerCastSystem;
    [SerializeField] private ChooseSpell chooseSpell;
    
    private Action<SendToOutputData> _isOutputData;
    
    void Start()
    {
        _isOutputData = End;
        
        gridSystem.Initialize();
        turnManager.Initialize(new SendToBattleData
        {
            PlayerUnits = unitsPlayer,
            EnemyUnits = unitsEnemy
        }, _isOutputData);
        
        playerCastSystem.InitializeSpells(spellsConfigs);
        chooseSpell.Initialize(spellsConfigs);
    }

    private void End(SendToOutputData data)
    {
        Debug.Log($"end data {data.ResultFight}");
        
        //Dispose all data and output send
    }
}
