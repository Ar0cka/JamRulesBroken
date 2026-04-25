using System.Collections.Generic;
using ScriptableObjects.WorldSceneConfigs;
using UnityEngine;

namespace WorldSceneScripts.Enemy
{
    public class EnemyMove : MonoBehaviour
    {
        [field:SerializeField] public EnemyWorldSceneConfig Config { get; private set; }
        [SerializeField] private List<Transform> patrolPoints;
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Rigidbody2D rb2D;

        [SerializeField] private float distance;

        private Transform _currentPlayerPosition;
        private Vector2 _currentPatrolPoint;
        private Queue<Vector2> _patrolVectorQueue = new();
        private bool _isPlayer;
        
        private void FixedUpdate()
        {
            if (!_isPlayer)
                MovePatrol();
            else
                MoveToPlayer();
            
            animator.SetBool(Config.Animations.walk, IsMove());
            spriteRenderer.flipX = rb2D.velocity.x < 0;
        }

        private void MovePatrol()
        {
            if (_patrolVectorQueue.Count == 0)
                _patrolVectorQueue = new Queue<Vector2>(patrolPoints.ConvertAll(x => (Vector2)x.position));
            
            if (_currentPatrolPoint == Vector2.zero || CheckDistance(_currentPatrolPoint))
            {
                _currentPatrolPoint = _patrolVectorQueue.Dequeue();
                _patrolVectorQueue.Enqueue(_currentPatrolPoint);
            }
            
            rb2D.velocity = (_currentPatrolPoint - rb2D.position).normalized * Config.EnemyStats.speed;
        }

        private void MoveToPlayer()
        {
            if (_currentPlayerPosition is null)
                return;

            if (CheckDistance(_currentPlayerPosition.position))
                return;
            
            var direction = (_currentPlayerPosition.position - transform.position).normalized;

            rb2D.velocity = direction * Config.EnemyStats.speed;
        }

        private bool CheckDistance(Vector2 targetPosition)
        {
            return Vector2.Distance(rb2D.position, targetPosition) <= distance;
        }

        private bool IsMove()
        {
            return Mathf.Abs(rb2D.velocity.x) > 0.1f || Mathf.Abs(rb2D.velocity.y) > 0.1f;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _currentPlayerPosition = other.transform;
                _isPlayer = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _currentPlayerPosition = null;
                _isPlayer = false;
            }
        }
    }
}