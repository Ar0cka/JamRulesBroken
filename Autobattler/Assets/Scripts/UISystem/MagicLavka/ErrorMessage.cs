using TMPro;
using UnityEngine;

namespace UISystem
{
    public class ErrorMessage : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI meshPro;
        
        private const string MoneyError = "Not enough money";
        private const string SpellError = "You already have such a spell";

        public void OpenPanel(ErrorType type, string message = "")
        {
            if (!string.IsNullOrEmpty(message))
                meshPro.text = message;
            else 
                meshPro.text = type == ErrorType.MoneyType ? MoneyError : SpellError;
            
            gameObject.SetActive(true);
        }
    }

    public enum ErrorType
    {
        MoneyType,
        SpellType
    }
}