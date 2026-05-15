using Grid;
using UnityEngine;

namespace Game.PatternCombat.Grid.Interfaces
{
    public interface ITurnGrid
    {
        public GridData GetFreeCells(int x, int y, float overlapRadius);
        public Vector2 GetPosition(int colum, int row);
    }

    public interface IRandomCells
    {
        
    }
}