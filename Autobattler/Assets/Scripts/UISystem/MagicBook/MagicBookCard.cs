using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MagicBook
{
    public class MagicBookCard : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI cardName;
        [SerializeField] private TextMeshProUGUI cardDescription;
        [SerializeField] private Image cardImage;


        public void Init(SpellConfig spellConfig)
        {
            cardImage.sprite = spellConfig.SpellIcon;
            cardName.text = spellConfig.SpellName;
            var spellStats = spellConfig.SpellStats;
            
            cardDescription.text =
                $"Type: {spellStats.spellType}\nElemental type: {spellStats.spellElement}\nDamage: {spellStats.spellDamage}";
        }
    }
}