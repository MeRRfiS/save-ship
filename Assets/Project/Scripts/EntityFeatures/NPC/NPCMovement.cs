using Assets.Project.Scripts.EntityFeatures.Interfaces;
using System;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Random = UnityEngine.Random;
using Transform = UnityEngine.Transform;

namespace Assets.Project.Scripts.EntityFeatures.NPC
{
    public class NPCMovement : IEntityMovement
    {
        private readonly Transform _transform;
        private readonly NavMeshAgent _agent;
        private readonly SpriteRenderer _spriteRenderer;
        private float _timer = 0f;
        private const float MaxTimer = 3f;
        private const float Radius = 20f;

        public event Action<float> OnMoving;

        public NPCMovement(NavMeshAgent agent, Transform transform, SpriteRenderer spriteRenderer)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            _agent = agent;
            _transform = transform;
            _spriteRenderer = spriteRenderer;
        }

        public void Move(Vector2 direction)
        {
            if (!_agent.enabled)
            {
                OnMoving?.Invoke(0f);
                return;
            }

            if (_agent.velocity.x > 0.01f)
            {
                _spriteRenderer.flipX = false;
            }
            else if (_agent.velocity.x < -0.01f)
            {
                _spriteRenderer.flipX = true;
            }

            if (!_agent.hasPath || _agent.remainingDistance < 0.1f)
            {
                OnMoving?.Invoke(0f);
                _timer -= Time.deltaTime;

                if (_timer <= 0f && _agent.enabled)
                {
                    Vector3 randomDestination = GetRandomPointAroundAgent();
                    _agent.SetDestination(randomDestination);
                    _timer = MaxTimer;
                }
            }
            else
            {
                OnMoving?.Invoke(1f);
            }
        }

        private Vector2 GetRandomPointAroundAgent()
        {
            Vector2 randomOffset = Random.insideUnitCircle * Radius;
            Vector3 randomPos = _transform.position + new Vector3(randomOffset.x, randomOffset.y, 0f);

            if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, Radius, _agent.areaMask))
            {
                return hit.position;
            }

            return _transform.position;
        }
    }
}