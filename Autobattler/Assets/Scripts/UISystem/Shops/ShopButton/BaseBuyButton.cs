using System;
using UnityEngine;

namespace UISystem.ShopButton
{
    public class BaseBuyButton<TConfig> : MonoBehaviour, IDisposable where TConfig : ScriptableObject
    {
        public int CurrentProductID { get; private set; }

        public void Initialize(TConfig conf)
        {
            
        }
        
        public void UpdateButtonData(TConfig config)
        {

        }

        public void Dispose()
        {
            
        }
    }
}