

using System.Collections;
using UnityEngine;

namespace BattleSystem
{
    public class BattleMove : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private Animator animator;

        private const float StoppingDistance = 0.1f;
        
        public IEnumerator Move(Vector2 targetPosition, float speed, string animationName)
        {
            animator.SetBool(animationName, true);
            
            while (Vector2.Distance(rigidbody2D.position, targetPosition) > StoppingDistance)
            {
                var direction = (targetPosition - rigidbody2D.position).normalized;
                rigidbody2D.velocity = direction * speed;
                
                yield return null;
            }

            rigidbody2D.position = targetPosition;
            rigidbody2D.velocity = Vector2.zero;

            animator.SetBool(animationName, false);

            yield return true;
        }
    }
}