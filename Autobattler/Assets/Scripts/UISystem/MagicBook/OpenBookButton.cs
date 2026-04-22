using System;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MagicBook
{
    public class OpenBookButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private MagicBookUI magicBook;

        private void Awake()
        {
            button.onClick.AddListener(() =>
            {
                magicBook.gameObject.SetActive(true);
                magicBook.OpenBook();
            });
        }
    }
}