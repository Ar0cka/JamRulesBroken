using UnityEngine;

namespace Game.Data
{
    public class EffectConfig : ScriptableObject
    {
        [field: SerializeField] public string EffectId { get; private set; }
        [field: SerializeField] public EffectType EffectType { get; private set; }
        [field: SerializeField] public Stats EffectiveStat { get; private set; }
    }
}