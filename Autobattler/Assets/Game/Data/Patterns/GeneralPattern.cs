using System.Collections.Generic;
using Game.Data.UnitConfigs;
using UnityEngine;

namespace Game.Data.Patterns
{
    [CreateAssetMenu(fileName =  "General Pattern", menuName = "Patterns/GeneralPattern")]
    public class GeneralPattern : DefaultPattern
    {
        [field: SerializeField] public List<UnitType> EffectedTypes { get; private set; }
    }
}