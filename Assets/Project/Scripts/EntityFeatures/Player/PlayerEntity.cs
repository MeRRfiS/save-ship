using Assets.Project.Scripts.EntityFeatures.Config;
using Assets.Project.Scripts.EntityFeatures.Interfaces;
using Assets.Project.Scripts.EntityFeatures.Models;
using UnityEngine;
using Zenject;

namespace Assets.Project.Scripts.EntityFeatures.Player
{
    public class PlayerEntity : MonoBehaviour, IEntity
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _takePoint;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;
        [SerializeField] private AudioSource _takeItemAudio;
        [SerializeField] private AudioSource _dropItemAudio;
        [SerializeField] private AudioSource _splashAudio;

        private Vector2 _direction;

        private IEntityMovement _playerMovement;
        private IEntityAct _playerAct;
        private IEntityAnimation _playerAnimation;
        [Inject] private PlayerTransform _playerTransform;

        private void Start()
        {
            _playerAnimation = new PlayerAnimation(_animator);
            _playerMovement = new PlayerMovement(_rigidbody, _playerConfig, _spriteRenderer);

            _playerMovement.OnMoving += _playerAnimation.MoveAnim;

            _playerTransform.Transform = transform;
            _playerAct = new PlayerAct(_playerConfig, _playerTransform, _takePoint, _takeItemAudio, _dropItemAudio, _splashAudio);

            _playerAct.OnActing += _playerAnimation.ActAnim;
        }

        private void Update()
        {
            _playerAct.FindActable();
        }

        private void FixedUpdate()
        {
            _playerMovement.Move(_direction);
        }

        public void SetEntityDirection(Vector2 direction)
        {
            _direction = direction;
        }

        public void EntityAct()
        {
            _playerAct.Act();
        }
    }
}