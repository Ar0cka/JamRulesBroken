using System.Collections;
using BattleSystem.UnitSystem.data;
using ScriptableObjects;
using UnityEngine;

namespace BattleSystem.BattleStategy
{
    public class MileAttack : UnitAttack
    {
        public override IEnumerator Attack(UnitController targetUnit, UnitBattleStates unitBattleStates)
        {
            SetSpriteSide(targetUnit.transform.position);
            
            var unitConfig = unitBattleStates.UnitConfig;

            animator.SetTrigger(unitConfig.Animation.attack);

            yield return new WaitForSeconds(animationTime);

            yield return StartCoroutine(targetUnit.TakeHit(unitConfig.Stats.attack * unitBattleStates.Count));

            yield return new WaitForSeconds(0.5f);
        }
    }
}