using System.Collections;
using BattleSystem.UnitSystem.data;
using ScriptableObjects;
using UnityEngine;

namespace BattleSystem.BattleStategy
{
    public class RangeAttack : UnitAttack
    {
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private Transform spawnPoint;


        public override IEnumerator Attack(UnitController targetUnit, UnitBattleStates unitBattleStates)
        {
            Vector2 targetVector = targetUnit.transform.position;
            SetSpriteSide(targetVector);
            
            var unitConfig = unitBattleStates.UnitConfig;
            
            animator.SetTrigger(unitConfig.Animation.attack);
            yield return new WaitForSeconds(animationTime);

            var arrow = Instantiate(tilePrefab, spawnPoint.position, Quaternion.identity);
            yield return StartCoroutine(arrow.GetComponent<RangeProjectileSettings>()
                .LaunchProjectileAndWait(targetVector));
            
            yield return StartCoroutine(targetUnit.TakeHit(unitConfig.Stats.attack * unitBattleStates.Count));
        }
    }
}