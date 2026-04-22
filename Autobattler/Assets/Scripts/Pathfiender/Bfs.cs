using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace DefaultNamespace.Pathfiender
{
    public class Bfs : MonoBehaviour
    {
        [SerializeField] private GridSystem gridSystem;
        
        public List<GridData> CalculatePath(int startX, int startY, int endX, int endY)
        {
            var queue = new Queue<(int x, int y)>();
            var cameFrom = new Dictionary<(int x, int y), (int x, int y)>();
            var visited = new HashSet<(int x, int y)>();
    
            queue.Enqueue((startX, startY));
            visited.Add((startX, startY));
    
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
        
                if (current.x == endX && current.y == endY)
                    break;
        
                foreach (var neighbor in gridSystem.GetNeighborCoords(current.x, current.y))
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        cameFrom[neighbor] = current;
                        queue.Enqueue(neighbor);
                    }
                }
            }
            
            var path = new List<GridData>();
            var step = (endX, endY);
    
            while (step.endX != startX || step.endY != startY)
            {
                path.Add(gridSystem.GridData[step.endX, step.endY]);
                if (!cameFrom.TryGetValue(step, out var value)) break;
                step = value;
            }
            path.Add(gridSystem.GridData[startX, startY]);
            path.Reverse();
    
            return path;
        }
    }
}