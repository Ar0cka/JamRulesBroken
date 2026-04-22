using System.Collections;
using BattleSystem.UnitSystem.data;
using ScriptableObjects;
using UnityEngine;

namespace BattleSystem.BattleStategy
{
    public class MileAttack : UnitAttack
    {
        public override IEnumerator Attack(UnitController targetUnit, UnitData unitData)
        {
            SetSpriteSide(targetUnit.transform.position);
            
            var unitConfig = unitData.UnitConfig;

            animator.SetTrigger(unitConfig.Animation.attack);

            yield return new WaitForSeconds(animationTime);

            yield return StartCoroutine(targetUnit.TakeHit(unitConfig.Stats.attack * unitData.Count));

            yield return new WaitForSeconds(0.5f);
        }
    }
}