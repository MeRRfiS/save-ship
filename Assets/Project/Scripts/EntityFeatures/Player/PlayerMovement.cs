using Assets.Project.Scripts.EntityFeatures.Config;
using Assets.Project.Scripts.EntityFeatures.Interfaces;
using System;
using UnityEngine;

namespace Assets.Project.Scripts.EntityFeatures.Player
{
    public class PlayerMovement : IEntityMovement
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly PlayerConfig _config;
        private readonly SpriteRenderer _spriteRenderer;


        public PlayerMovement(Rigidbody2D rigidbody, PlayerConfig config, SpriteRenderer spriteRenderer)
        {
            _rigidbody = rigidbody;
            _config = config;
            _spriteRenderer = spriteRenderer;
        }

        public event Action<float> OnMoving;

        public void Move(Vector2 direction)
        {
            if(direction.x < 0)
                _spriteRenderer.flipX = true;
            else if (direction.x > 0)
                _spriteRenderer.flipX = false;

            _rigidbody.linearVelocity = direction * _config.MoveSpeed;
            OnMoving?.Invoke(_rigidbody.linearVelocity.magnitude);
        }
    }
}
