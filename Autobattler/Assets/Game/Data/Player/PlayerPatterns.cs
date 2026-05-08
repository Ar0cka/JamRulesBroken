using System.Collections.Generic;
using Game.Data.Patterns;
using UnityEngine;

namespace Game.Data.Player
{
    [CreateAssetMenu(fileName = "Pattern collection", menuName = "Player/Patterns", order = 0)]
    public class PlayerPatterns : ScriptableObject
    {
        [field: SerializeField] public List<GeneralPattern> GeneralsPatterns { get; private set; }
        [field: SerializeField] public List<SamplePattern> SamplePatterns { get; private set; }
    }
}