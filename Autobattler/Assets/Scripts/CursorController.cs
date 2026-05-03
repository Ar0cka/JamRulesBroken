using System;
using Player;
using Player.PlayerProviders;
using Player.StateController;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class CursorController : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Vector2 hotspot;
        [SerializeField] private LayerMask targetMask;
        [SerializeField] private Texture2D shopCursorTexture;
        [SerializeField] private Texture2D magicShopCursorTexture;
        [SerializeField] private PlayerProvider playerProvider;

        private bool _cursorSet = false;

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject() || playerProvider.GetCurrentState(PlayerStates.IsDialogWindow))
            {
                UnsetCursor();
                return;
            }
            
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            var hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, targetMask);

            if (hit.collider != null && hit.collider.CompareTag("Shops"))
                SetShopCursor();
            else if (hit.collider != null && hit.collider.CompareTag("MageShop"))
                SetMagicShopCursor();
            else
                UnsetCursor();
        }
        
        private void SetShopCursor()
        {
            if (_cursorSet)
                return;
            
            Cursor.SetCursor(shopCursorTexture, hotspot, CursorMode.Auto);

            _cursorSet = true;
        }
        
        private void SetMagicShopCursor()
        {
            if (_cursorSet)
                return;

            Cursor.SetCursor(magicShopCursorTexture, hotspot, CursorMode.Auto);

            _cursorSet = true;
        }

        private void UnsetCursor()
        {
            if (!_cursorSet)
                return;

            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            
            _cursorSet = false;
        }
    }
}