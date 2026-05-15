using Game.Core.BaseTurnController;
using Game.PatternCombat;
using Game.PatternCombat.BattleUnitSystem;
using Game.PatternCombat.Grid.Services;
using Grid;
using UnityEngine;
using Zenject;

namespace ZenjectProviders
{
    public class CombatProvider : MonoInstaller
    {
        [SerializeField] private GridSystem gridSystem;
        
        public override void InstallBindings()
        {
            var unitRegister = new UnitsRegister();
            Container.Bind<IUnitRegister>().FromInstance(unitRegister).AsSingle();
            Container.Bind<IRegisterUpdate>().FromInstance(unitRegister).AsSingle();
            
            Container.Bind<TurnFactory>().AsSingle();
            Container.Bind<GridQuery>().AsSingle();
        }
    }
}