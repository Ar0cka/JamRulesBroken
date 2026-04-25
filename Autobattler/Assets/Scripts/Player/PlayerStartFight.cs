using System;
using SceneManagerWorld;
using UnityEngine;
using WorldSceneScripts.Enemy;

namespace Player
{
    public class PlayerStartFight : MonoBehaviour
    {
        [SerializeField] private PlayerFightAnimation animator;
        
        private bool _isFighting = false;

        public void FightIsEnd() => _isFighting = false;
        
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy") && !_isFighting)
            {
                _isFighting = true;
                var enemyMove = other.GetComponentInParent<EnemyMove>();
                
                if (enemyMove == null)
                    throw new Exception("EnemyMove is NULL");

                StartCoroutine(animator.StartFight(enemyMove.Config, enemyMove.gameObject));
            }
        }
    }
}