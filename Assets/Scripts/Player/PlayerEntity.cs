using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private SpriteRenderer _playerSprite;
        [SerializeField] private float _jumpForce;
        [SerializeField] private bool _onGround;
        [SerializeField] private Transform _groundChecker;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _groundCheckRadius;
        private bool _canInteract;
        private GameObject _interactableObject;
        private bool _faceRight;
        private Rigidbody2D _rigidbody;
        
        // Start is called before the first frame update
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _faceRight = true;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void FixedUpdate()
        {
            _onGround = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckRadius, _groundLayer);
        }

        private void SetDirection(float direction)
        {
            if ((_faceRight && direction < 0) || (!_faceRight && direction > 0))
            {
                Flip();
            }
        }

        private void Flip()
        {
            _faceRight = !_faceRight;
            _playerSprite.flipX = !_faceRight;
        }

        public void HorizontalMovement(float direction)
        {
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
