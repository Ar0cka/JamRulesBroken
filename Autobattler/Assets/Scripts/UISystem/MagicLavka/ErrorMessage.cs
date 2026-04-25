using System;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class ErrorMessage : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI meshPro;
        [SerializeField] private PlayerStateController stateController;
        [SerializeField] private Button exitButton;
        
        private const string MoneyError = "Not enough money";
        private const string SpellError = "You already have such a spell";

        private void Awake()
        {
            exitButton.onClick.AddListener(ClosePanel);
        }

        public void OpenPanel(ErrorType type, string message = "")
        {
            if (!string.IsNullOrEmpty(message))
                meshPro.text = message;
            else 
                meshPro.text = type == ErrorType.MoneyType ? MoneyError : SpellError;

            if (stateController is not null) 
                stateController.IsDialogWindow = true;
            
            gameObject.SetActive(true);
        }
        
        private void ClosePanel()
        {
            if (stateController is not null) 
                stateController.IsDialogWindow = false;
            
            gameObject.SetActive(false);
        }
    }

    public enum ErrorType
    {
        MoneyType,
        SpellType
    }
}