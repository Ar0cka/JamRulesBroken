using System;
using Player.Containers;
using ScriptableObjects;
using UISystem.ShopButton;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UISystem.Shops.ShopsFactory
{
    public class ShopsCardFactory
    {
        private readonly GameObject _magicCardPrefab;
        private readonly GameObject _tavernCardPrefab;

        public ShopsCardFactory(GameObject magicCardPrefab, GameObject tavernCardPrefab)
        {
            _magicCardPrefab = magicCardPrefab;
            _tavernCardPrefab = tavernCardPrefab;
        }
        
        public BaseBuyButton<TConfig> CreateBuyButton<TConfig>(ShopCardType cardType, TConfig config, Transform buyButtonParent, 
            BuySystemAbstract buySystem, IPlayerContainer playerContainer) where TConfig : class
        {
            var cardObject = SetNeededObject(cardType);

            if (cardObject == null)
                return null;
            
            var createdObject = Object.Instantiate(_magicCardPrefab, buyButtonParent, false);
            var baseBuyButton = createdObject.GetComponent<BaseBuyButton<TConfig>>();
            baseBuyButton.Initialize(config, playerContainer, buySystem);
            
            return baseBuyButton;
        }

        private GameObject SetNeededObject(ShopCardType cardType)
        {
            switch (cardType)
            {
                case ShopCardType.TavernCard:
                    return _tavernCardPrefab;
                case ShopCardType.MagicCard:
                    return _magicCardPrefab;
                default:
                    return null;
            }
        }
    }

    public enum ShopCardType
    {
        MagicCard,
        TavernCard
    }
}