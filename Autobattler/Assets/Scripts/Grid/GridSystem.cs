using Grid;
using UnityEngine;

namespace Grid
{
    public class GridSystem : MonoBehaviour
    {
        [SerializeField] private float cellSize;
        [SerializeField] private float stepInterval;
        
        [SerializeField] private GameObject hexagonPrefab;
        
        [SerializeField] private int cellsX, cellsY;
        
        private float _cellX, _cellY;
        private GridData[,] _gridData;

        public void Initialize()
        {
            _cellX = 2 * cellSize;
            _cellY = Mathf.Sqrt(3) * cellSize;
            
            _gridData = new GridData[cellsX, cellsY];
            
            CreateGrid();
        }
        
        private void CreateGrid()
        {
            var width = cellsX * _cellX * 0.75f;
            var height = cellsY * _cellY;
    
            Vector2 bottomLeft = (Vector2)transform.position 
                                 - Vector2.right * width / 2 
                                 - Vector2.up * height / 2;

            var stepX = _cellX * 0.75f;
    
            for (int x = 0; x < cellsX; x++)
            {
                var colOffsetY = (x % 2) * (_cellY / 2f);
        
                for (int y = 0; y < cellsY; y++)
                {
                    var worldX = bottomLeft.x + x * stepX;
                    var worldY = bottomLeft.y + y * _cellY + colOffsetY;
            
                    Instantiate(hexagonPrefab, 
                        new Vector3(worldX, worldY, transform.position.z), 
                        Quaternion.identity, new InstantiateParameters{parent = transform, worldSpace = true});
            
                    _gridData[x, y] = new GridData
                    {
                        WorldX = worldX, WorldY = worldY,
                        X = x, Y = y
                    };
                }
            }
        }

        public Vector2 GetPosition(int x, int y)
        {
            var gridData = _gridData[x, y];
            
            return new Vector2(gridData.WorldX, gridData.WorldY);
        }


#if UNITY_EDITOR
        [SerializeField] private float sphereRadius = 1f;
        
        public void OnDrawGizmos()
        {
            if (_gridData is { Length: > 0 })
            {
                foreach (var cell in _gridData)
                {
                    Gizmos.DrawSphere(new Vector3(cell.WorldX, cell.WorldY), 0.1f);
                }
            }
        }
#endif
    }
}