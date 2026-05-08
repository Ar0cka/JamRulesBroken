using System.Linq;
using Game.World.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Game.World.MagicBook
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