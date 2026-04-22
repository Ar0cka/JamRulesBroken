using System.Collections.Generic;
using System.Linq;
using BattleSystem;
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
        
        private HashSet<int> _usedPlayerSpawnPoints = new();
        private HashSet<int> _usedEnemyPoints = new();
        
        private float _cellX, _cellY;
        private GridData[,] _gridData;
        
        public GridData[,] GridData => _gridData;

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

        public Vector2 GetPosition(int colum, int row)
        {
            var gridData = _gridData[row, colum];
            
            return new Vector2(gridData.WorldX, gridData.WorldY);
        }
        
        public List<(int x, int y)> GetNeighborCoords(int x, int y)
        {
            var neighbors = new List<(int x, int y)>();
            
            if (x % 2 != 0)
            {
                neighbors.Add((x + 1, y));     // право
                neighbors.Add((x - 1, y));     // лево
                neighbors.Add((x, y + 1));     // верх
                neighbors.Add((x, y - 1));     // низ
                neighbors.Add((x + 1, y + 1)); // право-верх
                neighbors.Add((x - 1, y + 1)); // лево-верх
            }
            else
            {
                neighbors.Add((x + 1, y));     // право
                neighbors.Add((x - 1, y));     // лево
                neighbors.Add((x, y + 1));     // верх
                neighbors.Add((x, y - 1));     // низ
                neighbors.Add((x + 1, y - 1)); // право-низ
                neighbors.Add((x - 1, y - 1)); // лево-низ
            }
            
            return neighbors.Where(n => 
                n.x >= 0 && n.x < cellsX && 
                n.y >= 0 && n.y < cellsY).ToList();
        }

        public (int x, int y) GetRandomPlayerCell()
        {
            bool isFind = false;
            int randomY = 0;
            
            while (!isFind)
            {
                randomY = Random.Range(0, cellsY - 1);
                
                if (_usedPlayerSpawnPoints.Add(randomY))
                    isFind = true;
            }
            
            return (0, randomY);
        }

        public (int x, int y) GetRandomEnemyCell()
        {
            bool isFind = false;
            int randomY = 0;

            while (!isFind)
            {
                randomY = Random.Range(0, cellsY - 1);
                
                if (_usedEnemyPoints.Add(randomY))
                    isFind = true;
            }

            return (cellsX - 1, randomY);
        }

        public GridData GetFreeCells(int x, int y, float overlapRadius)
        {
            var neighbor = GetNeighborCoords(x, y);
            var freeCells = new List<GridData>();
            
            foreach (var n in neighbor)
            {
                var currentCell = _gridData[n.x, n.y];

#if UNITY_EDITOR
                targetPos = new Vector2(currentCell.WorldX, currentCell.WorldY);
#endif
                var hit = Physics2D.OverlapCircleAll(new Vector2(currentCell.WorldX, currentCell.WorldY),
                    overlapRadius);
                
                if (!hit.Any(h => h.CompareTag("Unit")))
                    freeCells.Add(currentCell);
            }

            if (freeCells.Count <= 0) return null;
            
            return freeCells[Random.Range(0, freeCells.Count)];
        }

#if UNITY_EDITOR
        [SerializeField] private float sphereRadius = 1f;
        [SerializeField] private bool drawGizmos = false;
        
        private Vector2 targetPos;
        public void OnDrawGizmos()
        {
            if (_gridData is { Length: > 0 } && drawGizmos)
            {
                foreach (var cell in _gridData)
                {
                    Gizmos.DrawSphere(new Vector3(cell.WorldX, cell.WorldY), 0.1f);
                }
            }
            
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(targetPos, 0.5f);
        }
#endif
    }
}