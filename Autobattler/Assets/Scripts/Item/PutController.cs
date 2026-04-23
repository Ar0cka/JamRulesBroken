using System;
using Player;
using ScriptableObjects;
using UISystem;
using UnityEngine;

namespace Item
{
    public class PutController : MonoBehaviour
    {
        [SerializeField] private ItemConfig itemConfig;
        [SerializeField] private EventPanelSettings putEffectWindow;
        [SerializeField] private PopText popManager;
        
        private bool _isPut;
        private bool _isRadius;
        
        private PlayerInventory _playerInventory;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && !_isPut && _isRadius)
            {
                _isPut = true;
                
                _playerInventory.AddItemToInventory(itemConfig);
                popManager.CreatePopText($"{itemConfig.ItemName} is put with count {1}");
                putEffectWindow.gameObject.SetActive(false);
                    
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                putEffectWindow.SetEventText();
                _playerInventory = other.gameObject.GetComponent<PlayerInventory>();
                _isRadius = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                putEffectWindow.gameObject.SetActive(false);
                _playerInventory = null;
                _isRadius = false;
            }
        }
    }
}