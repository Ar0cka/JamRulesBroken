using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.WorldSceneScripts.NpcDialogScript
{
    public class QuestCardUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI questTitleText;
        [SerializeField] private TextMeshProUGUI questDescriptionText;

        private QuestState _questState;
        private QuestData _questData;
        
        public void SetQuest(QuestData questData, int amount = 0)
        {
            _questState = QuestState.Active;
            
            _questData = questData;
            
            questTitleText.text = $"{_questState}: {_questData.questName}\n{_questData.neededItems.ItemName}: {amount}/{_questData.neededAmount}";
        }

        public void UpdateQuestProgress(int amountToAdd)
        {
            if (_questState == QuestState.Active)
            {
                if (amountToAdd >= _questData.neededAmount)
                {
                    _questState = QuestState.ReadyToComplete;
                }
                
                questTitleText.text = $"{_questState}: {_questData.questName}\n{_questData.neededItems.ItemName}: {amountToAdd}/{_questData.neededAmount}";
               // questDescriptionText.text = $"{_questData.questDescription}: {amountToAdd}/{_questData.neededAmount}";
            }
        }

        public void CompleteQuest()
        {
            _questState = QuestState.Completed;
            questTitleText.text = $"{_questState}:{_questData.questName}";
        }
    }

    public enum QuestState
    {
        Active,
        Completed,
        ReadyToComplete,
        Failed
    }
}