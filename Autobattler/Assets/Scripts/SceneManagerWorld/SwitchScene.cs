using System;
using System.Collections.Generic;
using System.Linq;
using BattleSystem;
using BattleSystem.UnitSystem;
using BattleSystem.UnitSystem.data;
using Player;
using ScriptableObjects;
using ScriptableObjects.WorldSceneConfigs;
using ShopSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using WorldSceneScripts.Enemy;

namespace SceneManagerWorld
{
    public class SwitchScene : MonoBehaviour
    {
        public static SwitchScene Instance;
        
        [SerializeField] private Camera camera;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private List<GameObject> disableGameObjects; 
        //[SerializeField] private List<TilemapCollider2D> tilemapsColliders;
        [SerializeField] private Collider2D playerCollider;
        [SerializeField] private GameObject grid;
        [SerializeField] private string sceneName;
        [SerializeField] private string nextScene;
        
        [SerializeField] private PlayerUnitContainer playerUnitContainer;
        [SerializeField] private PlayerSpellContainer playerSpellContainer;
        [SerializeField] private PlayerStartFight playerStartFight;
        [SerializeField] private Movement playerMovement;
        [SerializeField] private Wallet playerWallet;
        [SerializeField] private string mainScene;
        //TODO Dead window
        
        private EnemyWorldSceneConfig _enemyConfig;
        private GameObject _enemyObject;
        private Scene _scene;
        private Vector3 position = new(9999, 9999, 9999);
        private Vector3 savePos = Vector3.zero;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            Instance = this;
        }

        public void SwitchSceneAction(EnemyWorldSceneConfig enemyConfig, GameObject enemyObject)
        {
            camera.enabled = false;
            audioSource.enabled = false;

            playerMovement.ResetMovement();

            playerCollider.enabled = false;
            savePos = grid.transform.position;
            grid.transform.position = position;
           // foreach (var map in tilemapsColliders)
            //{
             //   map.enabled = false;
            //}
            
            foreach (var disableObjects in disableGameObjects)
            {
                if (disableObjects == null)
                    continue;
                
                disableObjects.SetActive(false);
            }

            _enemyConfig = enemyConfig;
            _enemyObject = enemyObject;

            if (nextScene is null)
                throw new NullReferenceException("nextScene name is null");
            
            SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive)!
                .completed += (op) => {
                _scene = SceneManager.GetSceneByName(nextScene);
                SceneManager.SetActiveScene(_scene);
                Debug.Log("Битва началась!");
            };
        }

        public SendToBattleData GetData()
        {
            var playerUnits = playerUnitContainer.PlayerUnits.Values.ToList();

            List<UnitBattleStates> unitData = new();

            grid.transform.position = savePos;
            foreach (var playerUnit in playerUnits)
            {
                unitData.Add(new UnitBattleStates(playerUnit.unitConfig, playerUnit.unitCount));
            }

            var playerSpells = playerSpellContainer.SpellDictionary.Values.ToList();

            List<UnitBattleStates> unitDataEnemy = new();

            foreach (var enemyUnit in _enemyConfig.EnemyBattle)
            {
                unitDataEnemy.Add(new UnitBattleStates(enemyUnit.units, enemyUnit.count));
            }

            return new SendToBattleData(unitData, unitDataEnemy, playerSpells);
        }

        public int GetMoney()
        {
            return _enemyConfig.EnemyStats.money;
        }

        public void TakeOutputData(SendToOutputData outputData)
        {
            if (outputData.ResultFight == FightResult.Lose)
            {
                SceneManager.UnloadSceneAsync(_scene);
                SceneManager.LoadScene(mainScene);
                Destroy(gameObject);
                return;
            }

            SceneManager.UnloadSceneAsync(_scene);
            playerCollider.enabled = true;
            
            foreach (var disabledObject in disableGameObjects)
            {
                if (disabledObject == null)
                    continue;
                
                disabledObject.SetActive(true);
            }
            
            camera.enabled = true;
            audioSource.enabled = true;
            
            playerStartFight.FightIsEnd();
            
            playerUnitContainer.RebuildUnitsAfterFight(outputData.UnitData);
            playerWallet.AddMoney(_enemyConfig.EnemyStats.money);

            _enemyConfig = null;
            Destroy(_enemyObject);
        }
    }
}