using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class BackHomeChecker : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                spriteRenderer.sortingOrder = 3;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                spriteRenderer.sortingOrder = 1;
            }
        }
    }
}