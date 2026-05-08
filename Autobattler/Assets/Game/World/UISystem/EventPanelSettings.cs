using TMPro;
using UnityEngine;

namespace UISystem
{
    public class EventPanelSettings : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI eventText;

        public void SetEventText(string text = "Input E for put item")
        {
            eventText.text = text;
            gameObject.SetActive(true);
        }
    }
}