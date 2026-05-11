using System;
using System.Collections.Generic;
using Game.Data.Patterns;
using UnityEngine;

namespace Game.Data.Player
{
    [CreateAssetMenu(fileName = "Pattern collection", menuName = "Player/Patterns", order = 0)]
    public class PlayerStartPatterns : ScriptableObject
    {
        [field:SerializeField] public PlayerCollection StartPlayerPatterns { get; private set; }

        public PlayerCollection Clone()
        {
            return new PlayerCollection
            {
                generalsPatterns = new List<GeneralPatternData>(StartPlayerPatterns.generalsPatterns),
                samplePatterns =  new List<SamplePatternData>(StartPlayerPatterns.samplePatterns)
            };
        }
    }

    [Serializable]
    public class PlayerCollection
    {
        public List<GeneralPatternData> generalsPatterns;
        public List<SamplePatternData> samplePatterns;
    }
}