using System.Collections;
using Game.Data.SpellConfigs;
using UnityEngine;

namespace BattleSystem
{
    public class SpellProjectail : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Animator spellAnimator;
        [SerializeField] private float distance;
        
        public IEnumerator Action(OldUnitController oldUnit, SpellConfig spellConfig, Vector2 targetPosition)
        {
            var animationConfig = spellConfig.SpellAnimations;
            
            spellAnimator.SetBool(animationConfig.flyAnimation, true);
            
            yield return StartCoroutine(Move(targetPosition, spellConfig.SpellStats.spellSpeed));
            
            spellAnimator.SetBool(animationConfig.flyAnimation, false);
            
            if (spellConfig.SpellData.spellType == SpellType.Damage)
            {
                StartCoroutine(oldUnit.TakeHit(spellConfig.SpellStats.spellDamage));
                Destroy(gameObject);
                yield break;
            }
            
            if (spellConfig.SpellData.spellType == SpellType.Heal)
            {
                oldUnit.Heal(spellConfig.SpellStats.spellDamage);
                Destroy(gameObject);
                yield break;
            }

            if (spellConfig.SpellData.spellType == SpellType.Effective)
            {
                oldUnit.SetEffective(spellConfig.EffectData, spellConfig);
                Destroy(gameObject);
                yield break;
            }
        }

        private IEnumerator Move(Vector2 targetPosition, float speed)
        {
            while (Vector2.Distance(rb.position, targetPosition) > distance)
            {
                rb.linearVelocity = (targetPosition - rb.position).normalized * speed;
                yield return null;
            }
        }
    }
}