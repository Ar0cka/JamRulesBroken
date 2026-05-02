using UnityEngine;

namespace UISystem
{
    public class BuySystemAbstract<TContainer, TConfig> : MonoBehaviour, IBuySystem
    {
        protected TConfig config;
        
        public void OpenBuyMenu()
        {
            
        }

        public void CloseBuyMenu()
        {
            
        }
    }
}