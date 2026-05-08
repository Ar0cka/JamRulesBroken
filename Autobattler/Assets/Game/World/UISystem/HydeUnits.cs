using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class HydeUnits : MonoBehaviour
    {
        [SerializeField] private Image unitImage;
        [SerializeField] private TextMeshProUGUI countText;
        
        public void SetUnit(Sprite sprite, int count)
        {
            unitImage.sprite = sprite;
            countText.text = count.ToString();
        }
    }
}