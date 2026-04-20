using System;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class CursorController : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Sprite cursorTexture;
        [SerializeField] private Vector2 hotspot;
        [SerializeField] private LayerMask targetMask;
        [SerializeField] private PlayerStateController playerStateController;
        private Texture2D _cursorTexture;

        private bool _cursorSet = false;
        
        private void Awake()
        {
            _cursorTexture = cursorTexture.texture;
        }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject() || playerStateController.IsShopOpen)
            {
                UnsetCursor();
                return;
            }
            
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            var hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, targetMask);

            if (hit.collider != null && hit.collider.CompareTag("Shops"))
                SetCursor();
            else
                UnsetCursor();
        }
        
        private void SetCursor()
        {
            if (_cursorSet)
                return;
            
            Cursor.SetCursor(_cursorTexture, hotspot, CursorMode.Auto);

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