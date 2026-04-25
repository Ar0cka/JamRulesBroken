using BattleSystem.UnitSystem;
using SceneManagerWorld;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BattleSystem
{
    public class ResultFight : MonoBehaviour
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private TextMeshProUGUI resultText;
        [SerializeField] private TextMeshProUGUI rewardText;
        private SendToOutputData _sendToOutputData;
        
        public void OpenPanel(SendToOutputData outputData, int moneyReward)
        {
            _sendToOutputData = outputData;
            
            resultText.text = $"Result fight: {_sendToOutputData.ResultFight}";

            if (_sendToOutputData.ResultFight == FightResult.Lose)
                moneyReward = 0;
                
            rewardText.text = $"Money reward: {moneyReward}";
            
            closeButton.onClick.AddListener(SwitchToMainScene);
        }

        private void SwitchToMainScene()
        {
            closeButton.onClick.RemoveListener(SwitchToMainScene);
            SwitchScene.Instance.TakeOutputData(_sendToOutputData);
        }
    }
}