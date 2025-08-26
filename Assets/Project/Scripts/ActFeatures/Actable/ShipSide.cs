using Assets.Project.Scripts.ActFeatures.Interfaces;
using System;
using UnityEngine;

namespace Assets.Project.Scripts.ActFeatures.Actable
{
    public class ShipSide : MonoBehaviour, IActable
    {
        public event Action OnDestroyObject;

        public void Acting(Transform actor)
        {
            if (actor == null) return;

            if (actor.TryGetComponent<IActable>(out var actable) && actable is MonoBehaviour mb)
            {
                Destroy(mb.gameObject);
            }
        }

        public void StopActing(Transform actor) { }

        public void IndicateInteractable() { }
        public void StopIndicate() { }
    }
}