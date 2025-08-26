using UnityEngine;

namespace Assets.Project.Scripts.EntityFeatures.Interfaces
{
    public interface IEntity
    {
        public void SetEntityDirection(Vector2 direction);
        public void EntityAct();
    }
}