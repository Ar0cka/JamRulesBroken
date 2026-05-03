using System;
using System.Collections.Generic;
using Player;
using Player.StateController;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.Tutor
{
    public class TutorController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> pages;
        [SerializeField] private GameObject questUI;
        [SerializeField] private PlayerStateController stateController;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button prevButton;
        [SerializeField] private Button closeBtn;

        private int currentPage = 0;

        private bool isOpen = false;
        
        private void Update()
        {
            nextButton.interactable = currentPage < pages.Count - 1;
            prevButton.interactable = currentPage > 0;
        }

        public void OpenPanel()
        {
            if (isOpen) return;
            
            gameObject.SetActive(true);
            
            if (questUI != null) 
                questUI.SetActive(false);

            // if (stateController != null)
            //     stateController.IsDialogWindow = true;
            
            nextButton.onClick.AddListener(() =>
            {
                pages[currentPage].SetActive(false);
                currentPage++;
                pages[currentPage].SetActive(true);
            });
            
            prevButton.onClick.AddListener(() =>
            {
                pages[currentPage].SetActive(false);
                currentPage--;
                pages[currentPage].SetActive(true);
            });
            
            closeBtn.onClick.AddListener(() =>
            {
                pages[currentPage].SetActive(false);
                currentPage = 0;
                pages[currentPage].SetActive(true);
                
                if (questUI != null)
                    questUI.SetActive(true);

                // if (stateController != null)
                //     stateController.IsDialogWindow = false;
                
                gameObject.SetActive(false);
                isOpen = false;
                closeBtn.onClick.RemoveAllListeners();
                prevButton.onClick.RemoveAllListeners();
                nextButton.onClick.RemoveAllListeners();
            });
        }
    }
}