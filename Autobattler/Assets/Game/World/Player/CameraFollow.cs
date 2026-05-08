using UnityEngine;

namespace Game.World.Player
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
    
        private void Update()
        {
            var moveDirection = new Vector3(target.position.x, target.position.y, -10f);

            transform.position = Vector3.MoveTowards(transform.position, moveDirection, 0.1f);
        }
    }
}
