using UnityEngine;

namespace Game.Data.UIConfigs
{
    [CreateAssetMenu(fileName = "PatternBookConfig", menuName = "Configs/PatternBookConfig")]
    public class PatternBookConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject PatternCard { get; private set; }
    }
}