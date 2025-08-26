using System;

namespace Assets.Project.Scripts.EntityFeatures.Interfaces
{
    public interface IEntityAct
    {
        public event Action<bool> OnActing;

        public void FindActable();
        public void Act();
        public void StopAct();
    }
}