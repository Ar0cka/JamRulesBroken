using Game.PatternCombat;
using Game.PatternCombat.BattleUnitSystem;
using Zenject;

namespace ZenjectProviders
{
    public class CombatProvider : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IRegisterUpdate>().To<UnitsRegister>().AsSingle().NonLazy();
        }
    }
}