using System.Collections.Generic;
using Game.Data.UnitConfigs;
using UnityEngine;

namespace Game.Data.Patterns
{
    [CreateAssetMenu(fileName = "SamplePattern", menuName = "Patterns/SamplePattern")]
    public class SamplePattern : DefaultPattern
    {
        [field: SerializeField] public List<UnitType> EffectedUnits { get; private set; }
    }
}