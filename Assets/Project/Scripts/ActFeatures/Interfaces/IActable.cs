using System;
using UnityEngine;

namespace Assets.Project.Scripts.ActFeatures.Interfaces
{
    public interface IActable
    {
        public event Action OnDestroyObject;

        public void Acting(Transform actor);
        public void StopActing(Transform actor);
        public void IndicateInteractable();
        public void StopIndicate();
    }
}