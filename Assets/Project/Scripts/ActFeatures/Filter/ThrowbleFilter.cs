using Assets.Project.Scripts.ActFeatures.Actable;
using Assets.Project.Scripts.ActFeatures.Interfaces;
using Assets.Project.Scripts.EntityFeatures.Models;
using Assets.Project.Scripts.EntityFeatures.NPC;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Project.Scripts.ActFeatures.Filter
{
    public class ThrowbleFilter : IActableFilter
    {
        private readonly PlayerTransform _playerTransform;

        public ThrowbleFilter(PlayerTransform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        public IActable GetCorrectActable(List<IActable> allActable)
        {
            if (allActable == null || allActable.Count == 0)
                return null;

            var throwable = allActable.Where(a => a is Furniture || a is NPCEntity).ToList();
            IActable nearest = null;
            float minDist = float.MaxValue;

            foreach (var actable in throwable)
            {
                var mono = actable as MonoBehaviour;
                if (mono == null) continue;

                float dist = Vector3.Distance(_playerTransform.Transform.position, mono.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = actable;
                }
            }

            return nearest;
        }
    }
}