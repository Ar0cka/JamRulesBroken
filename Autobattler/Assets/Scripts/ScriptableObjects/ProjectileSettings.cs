using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "projectile", menuName = "Config/Projectile")]
    public class ProjectileSettings : ScriptableObject
    {
        [field: SerializeField] public float ProjectileSpeed { get; private set; } = 10f;
        [field: SerializeField] public string ProjectileAnimation { get; private set; }
        [field: SerializeField] public GameObject ProjectileVfx { get; private set; }
    }
}