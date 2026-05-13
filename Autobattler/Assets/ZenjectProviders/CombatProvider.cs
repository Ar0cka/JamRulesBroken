using Game.Core.BaseTurnController;
using Game.PatternCombat;
using Game.PatternCombat.BattleUnitSystem;
using Zenject;

namespace ZenjectProviders
{
    public class CombatProvider : MonoInstaller
    {
        public override void InstallBindings()
        {
            var unitRegister = new UnitsRegister();
            Container.Bind<IUnitRegister>().FromInstance(unitRegister).AsSingle();
            Container.Bind<IRegisterUpdate>().FromInstance(unitRegister).AsSingle();
            Container.Bind<TurnFactory>().AsSingle();
        }
    }
}