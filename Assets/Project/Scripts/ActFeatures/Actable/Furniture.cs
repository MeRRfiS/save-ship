using Assets.Project.Scripts.ActFeatures.Interfaces;
using Assets.Project.Scripts.Core.Interfaces;
using Assets.Project.Scripts.Core.Managers;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Project.Scripts.ActFeatures.Actable
{
    public class Furniture : MonoBehaviour, IActable, IWeight
    {
        public float Weight => _weight;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private GameObject _outline;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private float _weight = 5f;
        private NavMeshObstacle _navMeshObstacle;

        public event Action OnDestroyObject;
        public event Action<IWeight> OnWeightChanged;
        [Inject] private IShipManager _shipManager;

        private void Start()
        {
            _navMeshObstacle = GetComponent<NavMeshObstacle>();
            _shipManager.AddWeightObject(this);
        }

        private void OnDestroy() 
        {
            if (UIManager.IsChangingScene)
            {
                return;
            }

            OnDestroyObject?.Invoke();
            OnWeightChanged?.Invoke(this);
        }

        public void Acting(Transform actor)
        {
            _navMeshObstacle.carving = false;
            _spriteRenderer.sortingOrder = 2;
            _collider.enabled = false;
            transform.SetParent(actor);
            transform.localPosition = Vector2.zero;
        }

        public void StopActing(Transform actor)
        {
            _navMeshObstacle.carving = true;
            _spriteRenderer.sortingOrder = 0;
            _collider.enabled = true;
            transform.SetParent(null);

            transform.position = ActableDropHelper.FindDropPosition(actor, transform.localScale.x * 2f);
        }

        public void IndicateInteractable() => _outline.SetActive(true);
        public void StopIndicate() => _outline.SetActive(false);
    }
}