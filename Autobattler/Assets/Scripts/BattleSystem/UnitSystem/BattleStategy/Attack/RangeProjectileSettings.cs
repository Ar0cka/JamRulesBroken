using System.Collections;
using ScriptableObjects;
using UnityEngine;

namespace BattleSystem.BattleStategy
{
    public class RangeProjectileSettings : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private ProjectileSettings projectileSettings;
        [SerializeField] private float stoppingDistance = 0.2f;

        public IEnumerator LaunchProjectileAndWait(Vector2 targetPosition)
        {
            while (Vector2.Distance(transform.position, targetPosition) > stoppingDistance)
            {
                var direction = (targetPosition - (Vector2)transform.position).normalized;
                rigidbody2D.velocity = direction * projectileSettings.ProjectileSpeed;
                
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);

                yield return null;
            }

            rigidbody2D.velocity = Vector2.zero;
            Destroy(gameObject);
        }
    }
}