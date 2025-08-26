using Assets.Project.Scripts.ActFeatures;
using Assets.Project.Scripts.ActFeatures.Actable;
using Assets.Project.Scripts.ActFeatures.Filter;
using Assets.Project.Scripts.ActFeatures.Interfaces;
using Assets.Project.Scripts.EntityFeatures.Config;
using Assets.Project.Scripts.EntityFeatures.Interfaces;
using Assets.Project.Scripts.EntityFeatures.Models;
using Assets.Project.Scripts.EntityFeatures.NPC;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts.EntityFeatures.Player
{
    public class PlayerAct : IEntityAct
    {
        private readonly PlayerTransform _playerTransform;
        private readonly Transform _takePoint;
        private readonly ActableFinder _finder;
        private readonly ActableIndicatorHandler _indicatorHandler = new();
        private readonly AudioSource _takeItem;
        private readonly AudioSource _dropItem;
        private readonly AudioSource _splash;

        private readonly Dictionary<Type, IActableFilter> _filters = new();

        private IActable _actingObject;
        private IActable _shipSideObject;

        public event Action<bool> OnActing;

        public PlayerAct(PlayerConfig config, PlayerTransform playerTransform, Transform takePoint, AudioSource takeItem, AudioSource dropItem, AudioSource splash)
        {
            _playerTransform = playerTransform;
            _takePoint = takePoint;
            _finder = new ActableFinder(config);

            _filters.Add(typeof(Furniture), new ThrowbleFilter(_playerTransform));
            _filters.Add(typeof(ShipSide), new ShipSideFilter());
            _filters.Add(typeof(NPCEntity), new ThrowbleFilter(_playerTransform));
            _takeItem = takeItem;
            _dropItem = dropItem;
            _splash = splash;
        }

        public void Act()
        {
            if (_shipSideObject != null)
            {
                _shipSideObject.Acting((_actingObject as MonoBehaviour)?.transform);
                if(_actingObject != null)
                    _splash.Play();
            }

            if (_actingObject != null)
            {
                StopAct();
                return;
            }

            if (_indicatorHandler.Current == null) return;

            _takeItem.Play();
            OnActing?.Invoke(true);
            var target = _indicatorHandler.Current;
            _indicatorHandler.SetCurrent(null);

            _actingObject = target;
            _actingObject.Acting(_takePoint);
        }

        public void StopAct()
        {
            _actingObject?.StopActing(_playerTransform.Transform);
            _actingObject = null;
            _dropItem.Play();
            OnActing?.Invoke(false);
        }

        public void FindActable()
        {
            var actables = _finder.FindNearbyActables(_playerTransform.Transform.position);

            _shipSideObject = _filters[typeof(ShipSide)].GetCorrectActable(actables);
            if (_actingObject != null) return;

            var nearestActable = _filters[typeof(Furniture)].GetCorrectActable(actables);
            _indicatorHandler.SetCurrent(nearestActable);
        }
    }
}
