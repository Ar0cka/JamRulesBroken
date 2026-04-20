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
        }

        public void AddMoney(int money)
        {
            CurrentMoney += money;
            moneyText.text = CurrentMoney.ToString();
        }
        
        public bool SpendMoney(int money)
        {
            if (CurrentMoney >= money)
            {
                CurrentMoney -= money;
                moneyText.text = CurrentMoney.ToString();
                return true;
            }
            
            return false;
        }
    }
}