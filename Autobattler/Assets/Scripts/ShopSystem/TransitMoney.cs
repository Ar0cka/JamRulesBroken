using UnityEngine;

namespace ShopSystem
{
    public class TransitMoney : MonoBehaviour
    {
        [SerializeField] private Wallet playerMoney;
        
        public bool PlayerBuy(int price)
        {
            if (playerMoney.CurrentMoney < price) return false;

            return playerMoney.SpendMoney(price);
        }
    }
}