using System;
using BattleSystem.UnitSystem.data;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Spell", menuName = "Spells/Spell Config")]
    public class SpellConfig : ScriptableObject
    {
        [field:SerializeField] public string SpellName { get; private set; }
        [field:SerializeField] public string Description { get; private set; }
        [field:SerializeField] public SpellStats SpellStats { get; private set; }
        [field:SerializeField] public Sprite SpellIcon { get; private set; }
        [field: SerializeField] public GameObject SpellVfx { get; private set;}
        [field: SerializeField] public EffectData EffectData { get; private set; }
        [field:SerializeField] public string AnimationName { get; private set; }
    }

    [Serializable]
    public class SpellStats
    {
        public SpellType spellType;
        public SpellElement spellElement;
        public SpellTarget spellTarget;
        public int spellDamage;
        public int spellCost;
        public float spellSpeed;
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