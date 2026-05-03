using System;
using Player.Containers;
using Player.PlayerProviders;
using UISystem;
using UISystem.Shops.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class PointerChecker : MonoBehaviour
    {
        [SerializeField] private Camera cameraMain;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private PlayerSpellContainer playerSpellContainer;
        [SerializeField] private PlayerUnitContainer playerUnitContainer;
        [SerializeField] private PlayerProvider playerProvider;
        
        
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
                    shop.EnterToShop(playerProvider, playerContainer);
                }
            }
        }
    }
}