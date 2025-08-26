using System;
using UnityEngine;

namespace Assets.Project.Scripts.EntityFeatures.Interfaces
{
    public interface IEntityMovement
    {
        public event Action<float> OnMoving;

        public void Move(Vector2 direction);
    }
}