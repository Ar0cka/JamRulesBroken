using System;
using UISystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class PointerChecker : MonoBehaviour
    {
        [SerializeField] private Camera cameraMain;
        [SerializeField] private LayerMask layerMask;
        
        public void Update()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                var mouserDirection = cameraMain.ScreenToWorldPoint(Input.mousePosition);
                
                var hit = Physics2D.Raycast(mouserDirection, Vector2.zero, Mathf.Infinity, layerMask);
                
                if (hit.collider != null && hit.collider.CompareTag("Shops"))
                {
                    var shop = hit.collider.gameObject.GetComponent<IShop>();
                    shop.EnterToShop();
                }
            }
        }
    }
}