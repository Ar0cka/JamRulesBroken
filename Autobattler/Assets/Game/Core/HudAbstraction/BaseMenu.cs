using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.HudAbstraction
{
    public abstract class BaseMenu<TData, TBaseConfig> : MonoBehaviour
        where TData : class 
        where TBaseConfig : ScriptableObject
    {
        [SerializeField] protected TBaseConfig config;

        protected TData Data;
        
        public abstract void Initialize(TData initializeData);
    }
}