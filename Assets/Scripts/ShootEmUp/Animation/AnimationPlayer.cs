using System;
using UnityEngine;
namespace ShootEmUp
{
    public class AnimationPlayer : MonoBehaviour
    {
        private readonly int _isAttackingB=Animator.StringToHash("isAttacking_b");
        private readonly int _isMovingB=Animator.StringToHash("isMoving_b");
        private readonly int _isBetterFireBallActiveB=Animator.StringToHash("isBetterFireBallActive_b");
        private readonly int _isChestOpenedB=Animator.StringToHash("isChestOpened_b");
        private readonly int _isStaffPulledB = Animator.StringToHash("isStaffPulled_b");
        private Animator _animator = null;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public void StartAttackingAnimation()
        {
            _animator.SetBool(_isAttackingB,true);
        }
        
        public void StartBetterAttackingAnimation()
        {
            _animator.SetBool(_isBetterFireBallActiveB,true);
        }

        public void StartMovingAnimation()
        {
            _animator.SetBool(_isMovingB,true);
        }
        
        
        public void StopAttackingAnimation()
        {
            _animator.SetBool(_isAttackingB,false);
        }
        public void StopBetterAttackingAnimation()
        {
            _animator.SetBool(_isBetterFireBallActiveB,false);
        }
        
        public void StopMovingAnimation()
        {
            _animator.SetBool(_isMovingB,false);
        }

        public void StartOpeningAnimation()
        {
            _animator.SetBool(_isChestOpenedB,true);
        }
        
        public void StartStaffPullingAnimation()
        {
            _animator.SetBool(_isStaffPulledB,true);
        }
        
        public void StopStaffPullingAnimation()
        {
            _animator.SetBool(_isStaffPulledB,false);
        }
    }
}
