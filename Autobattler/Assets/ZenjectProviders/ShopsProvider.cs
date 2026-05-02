using UISystem.Shops.ShopsFactory;
using UnityEngine;
using Zenject;

namespace ZenjectProviders
{
    public class ShopsProvider : MonoInstaller
    {
        [SerializeField] private GameObject magicShopCardPrefab;
        [SerializeField] private GameObject tavernShopCardPrefab;

        public override void InstallBindings()
        {
            var cardButtonFactory = new ShopsCardFactory(magicShopCardPrefab, tavernShopCardPrefab);
            Container.Bind<ShopsCardFactory>().FromInstance(cardButtonFactory).AsSingle();
        }
    }
}