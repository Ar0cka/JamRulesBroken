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
        
        public void SetQuest(QuestData questData)
        {
            _questState = QuestState.Active;
            
            _questData = questData;
            
            questTitleText.text = $"{_questState}: {questData.questName}";
            questTitleText.text = $"{_questData.questDescription}:0/{_questData.neededAmount}";
        }

        public void UpdateQuestProgress(int amountToAdd)
        {
            if (_questState == QuestState.Active)
            {
                if (amountToAdd >= _questData.neededAmount)
                {
                    _questState = QuestState.ReadyToComplete;
                    questTitleText.text = $"{_questState}: {_questData.questName}";
                    questTitleText.text = $"Ready to complete: {amountToAdd}/{_questData.neededAmount}";
                    return;
                }
                
                questTitleText.text = $"{_questState}: {_questData.questName}";
                questDescriptionText.text = $"{_questData.questDescription}:{amountToAdd}/{_questData.neededAmount}";
            }
        }

        public void CompleteQuest()
        {
            _questState = QuestState.Completed;

            questTitleText.text = $"{_questState}:{_questData.questName}";
            questDescriptionText.gameObject.SetActive(false);
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