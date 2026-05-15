using System;
using System.Collections.Generic;
using System.Linq;
using Game.Core.SceneManagerWorld.SendData;
using Game.Data.WorldSceneConfigs;
using Game.PatternCombat;
using Game.World.MoneySystem;
using Game.World.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Core.SceneManagerWorld
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

            var playerSpells = playerSpellContainer.SpellDictionary.Values.ToList();

            return new SendToBattleData(playerUnits, _enemyConfig.EnemyBattle, playerSpells);
        }

        public int GetMoney()
        {
            return _enemyConfig.EnemyStats.money;
        }

        public void TakeOutputData(SendToOutputData outputData)
        {
            if (outputData.fightResult == FightResult.Lose)
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
            
            playerUnitContainer.RebuildUnitsAfterFight(outputData.playerUnits);
            playerWallet.AddMoney(_enemyConfig.EnemyStats.money);

            _enemyConfig = null;
            Destroy(_enemyObject);
        }
    }
}