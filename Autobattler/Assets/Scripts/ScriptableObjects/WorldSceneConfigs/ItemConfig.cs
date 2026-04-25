using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu (fileName = "item", menuName = "Config/Item")]
    public class ItemConfig : ScriptableObject
    {
        [field:SerializeField] public string ItemName { get; private set; }
        [field:SerializeField] public ItemType ItemType { get; private set; }
        [field:SerializeField] public GameObject ItemPrefab { get; private set; }
    }

    public enum ItemType
    {
        None, 
        Quest
    }
}