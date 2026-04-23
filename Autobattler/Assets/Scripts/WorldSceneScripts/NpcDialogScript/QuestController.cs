using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.WorldSceneScripts.NpcDialogScript
{
    public class QuestController : MonoBehaviour
    {
        [SerializeField] private GameObject questCardPrefab;
        
        private Dictionary<string, QuestCardUI> _questCards;
        
        public void AcceptQuest(QuestData data)
        {
            
        }

        private void UpdateUI()
        {
            
        }
    }
}