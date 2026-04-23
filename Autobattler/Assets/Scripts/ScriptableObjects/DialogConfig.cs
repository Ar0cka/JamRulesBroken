using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "dialog", menuName = "World/Dialog", order = 0)]
    public class DialogConfig : ScriptableObject
    {
        [field:SerializeField] public string AcceptQuestText { get; private set; }
        [field:SerializeField] public string AfterAcceptText { get; private set; }
        [field:SerializeField] public string AwaitAcceptResultText { get; private set; }
        [field:SerializeField] public string NotNeededResultText { get; private set; }
        [field:SerializeField] public string QuestCompletedText { get; private set; }
        [field:SerializeField] public string AfterQuestText { get; private set; }
        [field:SerializeField] public string FailedText { get; private set; }
        [field:SerializeField] public QuestData QuestInfo { get; private set; }
    }

    [Serializable]
    public class QuestData
    {
        public string questName;
        public string questDescription;
        public ItemConfig neededItems;
        public int neededAmount;
    }

    public enum QuestType
    {
        Kill,
        Find
    }
}