

using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Grid;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace BattleSystem
{
    public class UnitMove : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer spriteRenderer;
        private const float StoppingDistance = 0.1f;
        
        public IEnumerator Move(Vector2 targetPath, float speed, string animationName)
        {
            animator.SetBool(animationName, true);
            
            while (Vector2.Distance(rigidbody2D.position, targetPath) > StoppingDistance)
            {
                var direction = targetPath - rigidbody2D.position;
                spriteRenderer.flipX = direction.x < 0;
                rigidbody2D.linearVelocity = direction.normalized * speed;
                
                yield return null;
            }
            
            rigidbody2D.position = targetPath;
            rigidbody2D.linearVelocity = Vector2.zero;

            animator.SetBool(animationName, false);

            yield return true;
        }
    }
}