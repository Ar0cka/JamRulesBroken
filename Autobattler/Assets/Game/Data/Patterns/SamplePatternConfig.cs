using System;
using UnityEngine;

namespace Game.Data.Patterns
{
    [CreateAssetMenu(fileName = "Sample Pattern", menuName = "Patterns/Sample", order = 0)]
    public class SamplePatternConfig : ScriptableObject
    {
        [field: SerializeField] public SamplePatternData SamplePatternData { get; private set; }
    }

    [Serializable]
    public class SamplePatternData : DefaultPattern
    {
        
    }
}