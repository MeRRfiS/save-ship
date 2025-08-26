using Assets.Project.Scripts.ShipFeature.Models;
using Zenject;

namespace Assets.Project.Scripts.Core.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ShipModel>().AsSingle().NonLazy();
        }
    }
}