using System;
using BattleSystem.UnitSystem.data;
using UnityEngine;

namespace ScriptableObjects.SpellConfigs
{
    [CreateAssetMenu(fileName = "New Spell", menuName = "Spells/Spell Config")]
    public class SpellConfig : ScriptableObject
    {
        [field:SerializeField] public int SpellID { get; private set; }
        [field:SerializeField] public SpellData SpellData { get; private set; }
        [field:SerializeField] public SpellStats SpellStats { get; private set; }
        [field:SerializeField] public SpellEffectData SpellSpellEffect { get; private set; }
        [field:SerializeField] public SpellVisualData SpellVisualData { get; private set; }
        [field:SerializeField] public SpellAnimations SpellAnimations { get; private set; }
        [field:SerializeField] public EffectData EffectData { get; private set; }
    }

    [Serializable]
    public class SpellData
    {
        public SpellType spellType;
        public string spellName;
        public string description;
    }

    [Serializable]
    public class SpellVisualData
    {
        public Sprite spellIcon;
        public GameObject spellObject;
    }

    [Serializable]
    public class SpellAnimations
    {
        public string startAnimation;
        public string flyAnimation;
        public string hitAnimation;
    }
    
    [Serializable]
    public class SpellStats
    {
        public int spellDamage;
        public int spellCost;
        public float spellSpeed;
    }

    [Serializable]
    public class SpellEffectData
    {
        public SpellElement spellElement;
        public SpellTarget spellTarget;
        public int effectTurns;
    }
    
    #region Enums

    public enum SpellType
    {
        Damage,
        Heal,
        Effective,
        Shield
    }
    
    public enum SpellElement
    {
        Fire,
        Water,
        Earth,
        Air,
        Light,
        Dark
    }
    
    public enum SpellTarget
    {
        Enemy,
        Self,
        Ally,
        Area
    }

    #endregion
}