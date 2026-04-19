using System.Collections;
using ScriptableObjects;
using UnityEngine;

namespace BattleSystem
{
    public class UnitController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private BattleMove move;
        [SerializeField] private UnitConfigs unitConfig;

        public IEnumerator Move(Vector2 targetPosition)
        {
            yield return StartCoroutine(move.Move(targetPosition, unitConfig.Movement.speed, unitConfig.Animation.walk));
            yield return true;
        }

        public IEnumerator Death()
        {
            animator.SetTrigger(unitConfig.Animation.death);
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
    }
}