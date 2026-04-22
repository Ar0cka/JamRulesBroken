using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class SpellCardSettings : MonoBehaviour
    {
        [SerializeField] private Button spellButton;
        [SerializeField] private TextMeshProUGUI spellName;

        public void Initialize(string nameSpell, Sprite sprite, Action<string> spellAction)
        {
            spellButton.image.sprite = sprite;
            spellName.text = nameSpell;
            
            spellButton.onClick.AddListener(() => spellAction?.Invoke(nameSpell));
        }
        
        public void Dispose()
        {
            spellButton.onClick.RemoveAllListeners();
        }
    }
}