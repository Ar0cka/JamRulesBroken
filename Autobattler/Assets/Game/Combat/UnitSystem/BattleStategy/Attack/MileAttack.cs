using System.Collections;
using BattleSystem.UnitSystem.data;
using UnityEngine;

namespace BattleSystem.BattleStategy
{
    public class MileAttack : UnitAttack
    {
        public override IEnumerator Attack(OldUnitController targetOldUnit, UnitCombatInfo unitCombatInfo)
        {
            SetSpriteSide(targetOldUnit.transform.position);
            
            var unitConfig = unitCombatInfo.UnitConfig;

            animator.SetTrigger(unitConfig.Animation.attack);

            yield return new WaitForSeconds(animationTime);

            yield return StartCoroutine(targetOldUnit.TakeHit(unitConfig.Stats.attack * unitCombatInfo.Count));

            yield return new WaitForSeconds(0.5f);
        }
    }
}