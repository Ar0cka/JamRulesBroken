using System.Collections.Generic;
using DefaultNamespace.Pathfiender;
using Game.PatternCombat.Grid.Interfaces;
using Grid;
using UnityEngine;

namespace Game.PatternCombat.TrunControllers
{
    public class PathService : IPathService
    {
        private Bfs _pathfinder;
        private ITurnGrid _turnGrid;

        public PathService(Bfs pathfinder, ITurnGrid turnGrid)
        {
            _pathfinder = pathfinder;
            _turnGrid = turnGrid;
        }

        public List<GridData> FindPath(int unitX, int unitY, int targetX, int targetY) => 
            _pathfinder.CalculatePath(unitX, unitY, targetX, targetY);
    }

    public interface IPathService
    {
        public List<GridData> FindPath(int unitX, int unitY, int targetX, int targetY);
    }
}