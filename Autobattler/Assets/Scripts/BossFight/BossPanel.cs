using Player;
using ScriptableObjects.WorldSceneConfigs;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.BossFight
{
    public class BossPanel : MonoBehaviour
    {
        [SerializeField] private Button flowerButton;
        [SerializeField] private Button fightButton;

        [SerializeField] private BossComics comics;
        [SerializeField] private PlayerFightAnimation playerFight;

        private bool Open;
        
        private EnemyWorldSceneConfig _currentEnemy;
        private GameObject _currentEnemyObject;
        
        public void OpenPanel(EnemyWorldSceneConfig enemyWorldSceneConfig, GameObject enemy)
        {
            if (Open) return;
            
            Open = true;
            
            _currentEnemy = enemyWorldSceneConfig;
            _currentEnemyObject = enemy;
            
            gameObject.SetActive(true);
            
            flowerButton.onClick.AddListener(Flower);
            fightButton.onClick.AddListener(Fight);
        }

        private void Fight()
        {
            playerFight.StartFightWithout(_currentEnemy, _currentEnemyObject);
            gameObject.SetActive(false);
            RemoveListener();
        }
        
        private void Flower()
        {
            comics.gameObject.SetActive(true);
            gameObject.SetActive(false);
            RemoveListener();
        }

        private void RemoveListener()
        {
            flowerButton.onClick.RemoveListener(Flower);
            fightButton.onClick.RemoveListener(Fight);
        }
    }
}