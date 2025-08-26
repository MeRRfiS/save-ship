using Assets.Project.Scripts.ActFeatures.Interfaces;
using Assets.Project.Scripts.EntityFeatures.Config;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts.ActFeatures
{
    public class ActableFinder
    {
        private readonly PlayerConfig _config;

        public ActableFinder(PlayerConfig config)
        {
            _config = config;
        }

        public List<IActable> FindNearbyActables(Vector2 position)
        {
            var hits = Physics2D.OverlapCircleAll(position, _config.InteractionRadius, _config.InteractableLayer);
            var actables = new List<IActable>();

            foreach (var hit in hits)
            {
                if (hit.TryGetComponent<IActable>(out var actable))
                {
                    actables.Add(actable);
                }
                if (hit.transform.parent != null && hit.transform.parent.TryGetComponent<IActable>(out var actablePar))
                {
                    actables.Add(actablePar);
                }
            }

            return actables;
        }
    }
}
