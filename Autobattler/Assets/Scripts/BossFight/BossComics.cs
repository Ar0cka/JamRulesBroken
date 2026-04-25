using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace.BossFight
{
    public class BossComics : MonoBehaviour
    {
        [SerializeField] private List<GameObject> pages;
        [SerializeField] private Button nextPage;
        [SerializeField] private Button prevPage;
        [SerializeField] private Button exit;

        [SerializeField] private string menuScene;
        
        private int currentPage = 0;

        private void Awake()
        {
            nextPage.onClick.AddListener(() =>
            {
                pages[currentPage].SetActive(false);
                currentPage++;
                pages[currentPage].SetActive(true);
            });
            
            prevPage.onClick.AddListener(() =>
            {
                pages[currentPage].SetActive(false);
                currentPage--;
                pages[currentPage].SetActive(true);
            });
            
            exit.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(menuScene);
            });
        }

        private void Update()
        {
            nextPage.interactable = currentPage < pages.Count - 1;
            prevPage.interactable = currentPage > 0;
        }
    }
}