using Assets.Project.Scripts.ActFeatures.Filter;
using Assets.Project.Scripts.ActFeatures.Interfaces;
using Assets.Project.Scripts.Core.Interfaces;
using Assets.Project.Scripts.Core.Managers;
using Assets.Project.Scripts.EntityFeatures.Models;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private ShipManager _shipManager;

    public override void InstallBindings()
    {
        BindPlayerTransformModel();
        BindUIManager();
        BindShipManager();
    }

    private void BindPlayerTransformModel()
    {
        Container.Bind<PlayerTransform>()
            .AsSingle()
            .NonLazy();
    }

    private void BindShipManager()
    {
        Container.Bind<IShipManager>().FromInstance(_shipManager).AsSingle();
    }

    private void BindUIManager()
    {
        Container.Bind<IUIManager>().FromInstance(_uiManager).AsSingle();
    }
}