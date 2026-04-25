using System;
using UnityEngine;

namespace Player
{
    public class PlayerStateController : MonoBehaviour
    {
        [SerializeField] private Camera cameraMain;
        
        public bool CanMove = false;
        public bool IsShopOpen = false;
        public bool IsBookOpen = false;
        public bool IsDialogWindow = false;

        private void Update()
        {
            var mouserVector = cameraMain.ScreenToWorldPoint(Input.mousePosition);
            
            var hit = Physics2D.Raycast(mouserVector, Vector2.zero);

            ChangeStates(hit);
        }
        
        private void ChangeStates(RaycastHit2D raycastHit2D)
        {
            if (raycastHit2D.collider is null && !CanMove)
            {
                CanMove = true;
                return;
            }
            
            if (raycastHit2D.collider !=null && raycastHit2D.collider.CompareTag("Shops") && CanMove)
                CanMove = false;
        }
    }
}