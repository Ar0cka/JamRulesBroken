using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using BattleSystem;
using BattleSystem.UnitSystem;
using BattleSystem.UnitSystem.data;
using Grid;
using SceneManagerWorld;
using ScriptableObjects;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BattleBootstrap : MonoBehaviour
{
    [SerializeField] private GridSystem gridSystem;
    [SerializeField] private TurnController turnManager;
    [SerializeField] private PlayerCastSystem playerCastSystem;
    [SerializeField] private ChooseSpell chooseSpell;

    [SerializeField] private ResultFight resultFight;
    
    private Action<SendToOutputData> _isOutputData;
    
    void Start()
    {
        if (SwitchScene.Instance == null)
            throw new NullReferenceException("Switch Scene is null");
        
        _isOutputData = End;
        
        gridSystem.Initialize();
        var data = SwitchScene.Instance.GetData();
        turnManager.Initialize(data, _isOutputData);
        
        playerCastSystem.InitializeSpells(data.PlayerSpells);
        chooseSpell.Initialize(data.PlayerSpells);
    }

    private void End(SendToOutputData data)
    {
        Debug.Log("End fight");
        resultFight.gameObject.SetActive(true);
        resultFight.OpenPanel(data, SwitchScene.Instance.GetMoney());
    }
}
