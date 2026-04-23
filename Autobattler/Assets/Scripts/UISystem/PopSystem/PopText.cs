using UnityEngine;

namespace UISystem
{
    public class PopText : MonoBehaviour
    {
        [SerializeField] private GameObject popTextPrefab;
        [SerializeField] private Transform popTextParent;

        public void CreatePopText(string text)
        {
            var instance = Instantiate(popTextPrefab, popTextParent, false);
            instance.transform.position = popTextParent.position;
            
            instance.GetComponent<PopObject>().LaunchPop(text);
        }
    }
}