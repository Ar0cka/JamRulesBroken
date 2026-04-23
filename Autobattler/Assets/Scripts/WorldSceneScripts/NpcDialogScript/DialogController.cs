using Player;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.WorldSceneScripts.NpcDialogScript
{
    public class DialogController : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject dialogWindow;
        [SerializeField] private TextMeshProUGUI dialogTextBox;
        [SerializeField] private TextMeshProUGUI acceptButtonTextBox;
        [SerializeField] private Button acceptButton;
        [SerializeField] private Button declineButton;
        
        [Header("Scripts")]
        [SerializeField] QuestController questController;

        [Header("Data")] 
        [SerializeField] private DialogConfig dialogConfig;

        [Header("Player")]
        [SerializeField] private PlayerInventory _playerInventory;

        private bool isAcceptedQuest = false;
        private bool isCompleted = false;
        
        private bool isDialogOpen = false;

        public void OpenDialog()
        {
            if (isDialogOpen) return;
            
            declineButton.onClick.AddListener(CloseDialog);
            
            if (isCompleted)
            {
                ShowText(dialogConfig.AfterQuestText, false);
                return;
            }
            if (!isAcceptedQuest)
            {
                acceptButton.onClick.AddListener(() =>
                {
                    questController.AcceptQuest();
                    ShowText(dialogConfig.AfterAcceptText, false);
                });
                
                ShowText(dialogConfig.AcceptQuestText);
                return;
            }
            if (isAcceptedQuest)
            {
                acceptButton.onClick.AddListener(EndQuest);
                ShowText(dialogConfig.AwaitAcceptResultText, true, "send items");
                return;
            }
        }

        private void EndQuest()
        {
            var itemFromInventory = _playerInventory.GetItemSlot(dialogConfig.QuestInfo.neededItems.ItemName);

            if (itemFromInventory == null)
            {
                ShowText($"{dialogConfig.NotNeededResultText}\n" +
                         $"{dialogConfig.QuestInfo.neededItems.ItemName}:0/{dialogConfig.QuestInfo.neededAmount}.", false);
                return;
            }

            if (itemFromInventory.Amount < dialogConfig.QuestInfo.neededAmount)
            {
                ShowText($"{dialogConfig.NotNeededResultText}\n" +
                         $"{dialogConfig.QuestInfo.neededItems.ItemName}:{itemFromInventory.Amount}/{dialogConfig.QuestInfo.neededAmount}.", false);
                return;
            }
                
            var deleted = _playerInventory.RemoveItem(dialogConfig.QuestInfo.neededItems, dialogConfig.QuestInfo.neededAmount);

            if (!deleted)
            {
                ShowText($"{dialogConfig.FailedText}", false);
                return;
            }
        }
        
        private void CloseDialog()
        {
            dialogWindow.SetActive(false);
            acceptButton.onClick.RemoveAllListeners();
            declineButton.onClick.RemoveAllListeners();
        }

        private void ShowText(string text, bool acceptButtonActive = true, string acceptButtonText = "accept")
        {
            dialogWindow.SetActive(true);
            dialogTextBox.text = text;
            acceptButton.gameObject.SetActive(acceptButtonActive);
            acceptButtonTextBox.text = acceptButtonText;

            isDialogOpen = true;
        }
    }
}