using System;
using UnityEngine;

namespace Player
{
    public class PutController : MonoBehaviour
    {
        [SerializeField] private GameObject putEffectWindow;
        [SerializeField] private KeyCode putKey;
        
        private bool _isPutActive;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                putEffectWindow.SetActive(true);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (Input.GetKeyDown(putKey))
                {
                    var playerInventory = other.gameObject.GetComponent<PlayerInventory>();
                    
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                putEffectWindow.SetActive(false);
            }
        }
    }
}