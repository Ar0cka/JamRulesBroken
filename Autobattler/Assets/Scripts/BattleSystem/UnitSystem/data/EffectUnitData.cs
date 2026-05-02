using System;
using ScriptableObjects;
using ScriptableObjects.SpellConfigs;

namespace BattleSystem.UnitSystem.data
{
    [Serializable]
    public class EffectUnitData
    {
        public EffectType EffectType;
        public int TurnsLess;
        public SpellConfig CurrentSpellData;
        
        public void SetNewEffect(EffectType effectType, int turnsLess, SpellConfig spellData)
        {
            EffectType = effectType;
            TurnsLess = turnsLess;
            CurrentSpellData = spellData;
        }

        public void EffectTurnPassed()
        {
            TurnsLess--;
            
            if (TurnsLess <= 0)
                EffectType = EffectType.None;
        }
    }
}