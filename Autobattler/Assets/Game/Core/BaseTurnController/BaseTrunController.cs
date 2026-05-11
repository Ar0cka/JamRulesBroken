using DefaultNamespace.Pathfiender;
using Game.Combat.Grid;
using Grid;
using UnityEngine;
using Zenject;

namespace Game.Core.BaseTurnController
{
    public abstract class BaseTurnController : MonoBehaviour
    {
        [Inject] protected Bfs Pathfinder;
        
        protected ITurnGrid TurnGrid;

        public abstract void InitializeTurnController(ITurnGrid turnGrid);

        protected virtual void CreateTurn()
        {
            
        }
    }
}