using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.World.BackCheckerSystems
{
    public class BackTilemaps : MonoBehaviour
    {
        [SerializeField] private TilemapRenderer tilemapRenderer;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                tilemapRenderer.sortingOrder = 3;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                tilemapRenderer.sortingOrder = 1;
            }
        }
    }
}