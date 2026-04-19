using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Player", menuName = "Config/Player", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float speed = 5f;
        public float Speed => speed;
    }
}