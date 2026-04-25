using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace.Menu
{
    public class ButtonManager : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button exitButton;

        [SerializeField] private string nextScene;
        
        private void Awake()
        {
            playButton.onClick.AddListener(Play);
            exitButton.onClick.AddListener(Exit);
        }

        private void Play()
        {
            playButton.onClick.RemoveAllListeners();
            exitButton.onClick.RemoveAllListeners();
            SceneManager.LoadScene(nextScene);
        }
        
        private void Exit()
        {
            playButton.onClick.RemoveAllListeners();
            exitButton.onClick.RemoveAllListeners();
            Application.Quit();
        }
    }
}