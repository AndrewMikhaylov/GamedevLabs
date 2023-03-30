using System;
using System.Collections;
using System.Collections.Generic;
using Core.Enums;
using Core.Movement.Data;
using Core.Tools;
using DefaultNamespace;
using Player.PlayerAnimation;
using StatsSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    
    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private AnimationController _animator;

        [SerializeField] private DirectionalMovementData _directionalMovementData;

        [SerializeField] private bool _onGround;
        [SerializeField] private Transform _groundChecker;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _groundCheckRadius;

        [SerializeField] private DirectionalCameraPair _cameras;

        private DirectionalMover _directionalMover;
        private Jumper _jumper;
        
        private Rigidbody2D _rigidbody;

        
        // Start is called before the first frame update
        public void Initialize(IStatValueGiver statValueGiver)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            
            _directionalMover = new DirectionalMover(_rigidbody, _directionalMovementData, statValueGiver);
            _jumper = new Jumper(_rigidbody, statValueGiver);
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimations();
            UpdateCameras();
        }

        private void UpdateCameras()
        {
            foreach (var cameraPair in _cameras.DirectionalCameras)
            {
                cameraPair.Value.enabled = cameraPair.Key == _directionalMover.Direction;
            }
        }

        private void UpdateAnimations()
        {
            _animator.PlayAnimation(AnimationType.Idle, true);
            _animator.PlayAnimation(AnimationType.Run, _directionalMover.IsMoving);
            _animator.PlayAnimation(AnimationType.Jump, !_onGround);
            _animator.PlayAnimation(AnimationType.Fall, _rigidbody.velocity.y<0&&!_onGround);
        }

        private void FixedUpdate()
        {
            _onGround = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckRadius, _groundLayer);
        }

        public void HorizontalMovement(float direction) => _directionalMover.HorizontalMovement(direction);


        public void Jump() => _jumper.Jump(_onGround);
        

        public void StartAttack()
        {
            if (!_animator.PlayAnimation(AnimationType.Attack1, true))
            {
                return;
            }

            _animator.ActionRequested += Attack;
            _animator.ActionEnded += EndAttack;
        }

        private void Attack()
        {
            Debug.Log("Attack");
        }

        private void EndAttack()
        {
            _animator.ActionRequested -= Attack;
            _animator.ActionEnded -= EndAttack;
            _animator.PlayAnimation(AnimationType.Attack1, false);
        }
    }
}
