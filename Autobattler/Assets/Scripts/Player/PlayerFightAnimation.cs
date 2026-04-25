using System.Collections;
using SceneManagerWorld;
using ScriptableObjects.WorldSceneConfigs;
using UnityEngine;
using WorldSceneScripts.Enemy;

namespace Player
{
    public class PlayerFightAnimation : MonoBehaviour
    {
        [SerializeField] private string attackTrigger = "Attack";
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private float fightDelay = 0.5f;

        public void StartFightWithout(EnemyWorldSceneConfig sceneConfig, GameObject enemyObj)
        {
            SwitchScene.Instance.SwitchSceneAction(sceneConfig, enemyObj);
        }
        
        public IEnumerator StartFight(EnemyWorldSceneConfig sceneConfig, GameObject enemyObj)
        {
            playerAnimator.SetTrigger(attackTrigger);
            yield return new WaitForSeconds(fightDelay);
            
            SwitchScene.Instance.SwitchSceneAction(sceneConfig, enemyObj);
        }
    }
}