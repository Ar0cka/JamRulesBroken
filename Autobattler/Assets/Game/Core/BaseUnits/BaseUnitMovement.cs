using System;
using Cysharp.Threading.Tasks;
using Game.Data.UnitConfigs;
using Grid;
using UnityEngine;

namespace Game.Core.BaseUnits
{
    public abstract class BaseUnitMovement : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D rb2D;
        [SerializeField] protected SpriteRenderer spriteRenderer;
        [SerializeField] protected float stopDistance = 0.1f;
        
        protected bool IsMoving;
        protected Vector2 CurrentTarget;

        protected UnitMovement MoveConfig;

        protected virtual void FixedUpdate()
        {
            if (!IsMoving || CurrentTarget == Vector2.zero)
                return;

            if (Vector2.Distance(rb2D.position, CurrentTarget) > stopDistance)
            {
                Stop();
                return;
            }
            
            Vector2 direction = (CurrentTarget- rb2D.position).normalized;
            
            SetSpriteSide(direction);
            
            rb2D.linearVelocity = direction * MoveConfig.speed;
        }

        public virtual async UniTask MoveAsync(GridData targetGridData, UnitMovement moveConfig)
        {
            CurrentTarget = targetGridData.WorldPosition;
            MoveConfig = moveConfig;

            await UniTask.WaitUntil(() => !IsMoving);
        }

        protected void SetSpriteSide(Vector2 currentDirection)
        {
            spriteRenderer.flipX = currentDirection.x > 0;
        }
        
        protected void Stop()
        {
            IsMoving = false;
            rb2D.linearVelocity = Vector2.zero;
            CurrentTarget = Vector2.zero;
        }
    }
}