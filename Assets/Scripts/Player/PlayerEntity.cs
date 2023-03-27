using System;
using System.Collections;
using System.Collections.Generic;
using Core.Enums;
using Core.Tools;
using Player.PlayerAnimation;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    
    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private AnimationController _animator;
        
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private SpriteRenderer _playerSprite;
        [SerializeField] private float _jumpForce;
        [SerializeField] private bool _onGround;
        [SerializeField] private Transform _groundChecker;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _groundCheckRadius;

        [SerializeField] private DirectionalCameraPair _cameras;
        
        private Direction _direction;
        private Rigidbody2D _rigidbody;
        private Vector2 _movement;
        
        // Start is called before the first frame update
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _direction = Direction.Right;
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimations();
        }

        private void UpdateAnimations()
        {
            _animator.PlayAnimation(AnimationType.Idle, true);
            _animator.PlayAnimation(AnimationType.Run, _movement.magnitude>0);
            _animator.PlayAnimation(AnimationType.Jump, !_onGround);
        }

        private void FixedUpdate()
        {
            _onGround = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckRadius, _groundLayer);
        }

        private void SetDirection(float direction)
        {
            if ((_direction == Direction.Right && direction < 0) || (_direction == Direction.Left && direction > 0))
            {
                Flip();
            }
        }

        private void Flip()
        {
            _direction = _direction == Direction.Right ? Direction.Left : Direction.Right;
            _playerSprite.flipX = _direction != Direction.Right;
            foreach (var cameraPair in _cameras.DirectionalCameras)
            {
                cameraPair.Value.enabled = cameraPair.Key == _direction;
            }
        }

        public void HorizontalMovement(float direction)
        {
            _movement.x = direction;
            SetDirection(direction);
            Vector2 velocity = _rigidbody.velocity;
            velocity.x = direction * _horizontalSpeed;
            _rigidbody.velocity = velocity;
        }

        public void Jump()
        {
            if (!_onGround)
            {
                return;
            }
            
            _rigidbody.AddForce(Vector2.up*_jumpForce);

        }
    }
}
