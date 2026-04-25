using System;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UISystem.Tutor
{
    public class EcsMenu : MonoBehaviour
    {
        [SerializeField] private PlayerStateController stateController;
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
                stateController.IsDialogWindow = true;
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
                stateController.IsDialogWindow = false;
                dialogWindow.SetActive(false);
            });
        }
    }
}