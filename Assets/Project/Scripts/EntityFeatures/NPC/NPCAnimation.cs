using Assets.Project.Scripts.EntityFeatures.Interfaces;
using UnityEngine;

namespace Assets.Project.Scripts.EntityFeatures.NPC
{
    public class NPCAnimation : IEntityAnimation
    {
        private readonly Animator _animator;

        public NPCAnimation(Animator animator)
        {
            _animator = animator;
        }

        public void ActAnim(bool isAct) { }

        public void MoveAnim(float speed)
        {
            _animator.SetFloat("Speed", speed);
        }
    }
}
