using Assets.Project.Scripts.ActFeatures;
using Assets.Project.Scripts.ActFeatures.Interfaces;
using Assets.Project.Scripts.Core.Interfaces;
using Assets.Project.Scripts.Core.Managers;
using Assets.Project.Scripts.EntityFeatures.Interfaces;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Project.Scripts.EntityFeatures.NPC
{
    public class NPCEntity : MonoBehaviour, IEntity, IActable, IWeight
    {
        [SerializeField] private GameObject _outline;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _weight;
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private IEntityMovement _movement;
        private IEntityAnimation _animation;

        public float Weight => _weight;
        protected bool isSceneChanging = false;
        public event Action OnDestroyObject;
        public event Action<IWeight> OnWeightChanged;

        [Inject] protected IShipManager _shipManager;

        private void Awake()
        {
            _animation = new NPCAnimation(_animator);
            _movement = new NPCMovement(_agent, transform, _spriteRenderer);
            _movement.OnMoving += _animation.MoveAnim;
        }

        protected virtual void Start()
        {
            _shipManager.AddWeightObject(this);
        }

        private void Update()
        {
            _movement.Move(Vector2.zero);
        }

        protected virtual void OnDestroy()
        {
            if (UIManager.IsChangingScene)
            {
                return;
            }

            OnDestroyObject?.Invoke();
            OnWeightChanged?.Invoke(this);
        }

        public void EntityAct() { }

        public void SetEntityDirection(Vector2 direction) { }

        public void Acting(Transform actor)
        {
            _spriteRenderer.sortingOrder = 2;
            _agent.enabled = false;
            transform.SetParent(actor);
            transform.localPosition = Vector2.zero;
        }

        public void StopActing(Transform actor)
        {
            _spriteRenderer.sortingOrder = 0;
            _agent.enabled = true;
            transform.SetParent(null);

            transform.position = ActableDropHelper.FindDropPosition(actor, transform.localScale.x * 2f);
        }

        public void IndicateInteractable() => _outline.SetActive(true);
        public void StopIndicate() => _outline.SetActive(false);
    }
}