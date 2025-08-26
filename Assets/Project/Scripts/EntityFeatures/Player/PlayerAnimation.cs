using Assets.Project.Scripts.EntityFeatures.Interfaces;
using UnityEngine;

namespace Assets.Project.Scripts.EntityFeatures.Player
{
    public class PlayerAnimation : IEntityAnimation
    {
        private readonly Animator _animator;

        public PlayerAnimation(Animator animator)
        {
            _animator = animator;
        }

        public void ActAnim(bool isAct)
        {
            _animator.SetBool("IsAct", isAct);
        }

        public void MoveAnim(float speed)
        {
            _animator.SetFloat("Speed", speed);
        }
    }
}
