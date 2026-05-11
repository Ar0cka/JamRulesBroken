using System;
using UnityEngine;

namespace Game.Data.Player
{
    [CreateAssetMenu(fileName = "pattern settings", menuName = "Patterns/PlayerPatternSettings")]
    public class PatternLimits : ScriptableObject
    {
        [field:SerializeField] public PlayerPatternSettings PlayerPatternSettings { get; private set; }

        public PlayerPatternSettings Clone()
        {
            return new PlayerPatternSettings
            {
                maxGeneralPatterns = PlayerPatternSettings.maxGeneralPatterns,
                maxSamplePatterns = PlayerPatternSettings.maxSamplePatterns
            };
        }
    }

    [Serializable]
    public class PlayerPatternSettings
    {
        public int maxSamplePatterns;
        public int maxGeneralPatterns;
    }
}