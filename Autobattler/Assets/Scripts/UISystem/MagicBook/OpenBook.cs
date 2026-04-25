using System;
using System.Linq;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MagicBook
{
    public class OpenBook : MonoBehaviour
    {
        [SerializeField] private BookUI book;
        [SerializeField] private Button openButton;
        [SerializeField] private PlayerSpellContainer playerSpells;
        
        private void Awake()
        {
            openButton.onClick.AddListener(() => book.Open(playerSpells.SpellDictionary.Values.ToList()));
        }
    }
}