using System;
using UnityEngine;

namespace Game.Data.Patterns
{
    [CreateAssetMenu(fileName = "General Pattern", menuName = "Patterns/General", order = 0)]
    public class GeneralPatternConfig : ScriptableObject
    {
        [field: SerializeField] public GeneralPatternData Data { get; private set; }
    }

    [Serializable]
    public class GeneralPatternData : DefaultPattern
    {
        
    }
}