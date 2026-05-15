using Grid;
using UnityEngine;

namespace Game.PatternCombat.Grid.Services
{
    public class GridQuery
    {
        public bool IsAdjacent4(GridData target, GridData current)
        {
            int dx = Mathf.Abs(target.X - current.X);
            int dy = Mathf.Abs(target.X - current.X);

            return (dx == 1 && dy == 0) || (dx == 0 && dy == 1);
        }

        public bool IsAdjacent8(GridData target, GridData current)
        {
            int dx = Mathf.Abs(target.X - current.X);
            int dy = Mathf.Abs(target.Y - current.Y);

            return dx <= 1 && dy <= 1 && !(dx == 0 && dy == 0);
        }

        public int Manhattan(GridData target, GridData current)
        {
            return Mathf.Abs(target.X - current.X) + Mathf.Abs(target.Y - current.Y);
        }
    }
}