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
                generalsPatterns = new List<GeneralPattern>(StartPlayerPatterns.generalsPatterns),
                samplePatterns =  new List<SamplePattern>(StartPlayerPatterns.samplePatterns)
            };
        }
    }

    [Serializable]
    public class PlayerCollection
    {
        public List<GeneralPattern> generalsPatterns;
        public List<SamplePattern> samplePatterns;
    }
}