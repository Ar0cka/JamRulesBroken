using System;
using Player;
using Player.PlayerProviders;
using Player.StateController;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UISystem.Tutor
{
    public class EcsMenu : MonoBehaviour
    {
        [SerializeField] private PlayerProvider playerProvider;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button returnButton;
        [SerializeField] private GameObject dialogWindow;

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (dialogWindow.activeSelf)
                    return;
                
                dialogWindow.SetActive(true);
                playerProvider.SwitchPlayerState(PlayerStates.IsDialogWindow, true);
            }
        }

        private void Awake()
        {
            exitButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(0);
            });
            
            returnButton.onClick.AddListener(() =>
            {
                playerProvider.SwitchPlayerState(PlayerStates.IsDialogWindow, false);
                dialogWindow.SetActive(false);
            });
        }
    }
}