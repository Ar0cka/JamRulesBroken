using System.Collections;
using BattleSystem.UnitSystem.data;
using UnityEngine;

namespace BattleSystem.BattleStategy
{
    public abstract class UnitAttack : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] protected SpriteRenderer spriteRenderer;
        [SerializeField] protected Animator animator;

        [Header("Settings")] 
        [SerializeField] protected float animationTime;

        public abstract IEnumerator Attack(OldUnitController targetOldUnit, UnitCombatInfo unitConfig);

        protected void SetSpriteSide(Vector2 targetVector)
        {
            var direction = targetVector - (Vector2)transform.position;
            spriteRenderer.flipX = direction.x < 0;
        }
    }
}