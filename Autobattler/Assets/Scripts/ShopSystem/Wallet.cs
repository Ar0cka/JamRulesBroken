using System;
using TMPro;
using UnityEngine;

namespace ShopSystem
{
    public class Wallet : MonoBehaviour
    {
        [SerializeField] private int startMoney;
        [SerializeField] private TextMeshProUGUI moneyText;
        
        public int CurrentMoney { get; set; }

        private void Start()
        {
            CurrentMoney = startMoney;
            moneyText.text = $"Money: {CurrentMoney}";
        }

        public void AddMoney(int money)
        {
            CurrentMoney += money;
            moneyText.text = $"Money: {CurrentMoney}";
        }
        
        public bool SpendMoney(int money)
        {
            if (CurrentMoney >= money)
            {
                CurrentMoney -= money;
                moneyText.text = $"Money: {CurrentMoney}";
                return true;
            }
            
            return false;
        }
    }
}