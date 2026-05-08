using Game.Core.ShopAbstract.Interfaces;
using Game.World.Player.Interfaces;
using Game.World.Player.PlayerProviders;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.World.Player
{
    public class PointerChecker : MonoBehaviour
    {
        [SerializeField] private Camera cameraMain;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private PlayerSpellContainer playerSpellContainer;
        [SerializeField] private PlayerUnitContainer playerUnitContainer;
        [SerializeField] private PlayerStatesProvider playerStatesProvider;
        
        
        public void Update()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                var mouserDirection = cameraMain.ScreenToWorldPoint(Input.mousePosition);
                
                var hit = Physics2D.Raycast(mouserDirection, Vector2.zero, Mathf.Infinity, layerMask);
                
                if (hit.collider != null && (hit.collider.CompareTag("Shops") || hit.collider.CompareTag("MageShop")))
                {
                    IPlayerContainer playerContainer =
                        hit.collider.CompareTag("Shops") ? playerUnitContainer : playerSpellContainer;
                    
                    var shop = hit.collider.gameObject.GetComponent<IShop>();
                    shop.EnterToShop(playerStatesProvider, playerContainer);
                }
            }
        }
    }
}