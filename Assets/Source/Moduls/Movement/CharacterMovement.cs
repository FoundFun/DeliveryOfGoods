using System;
using UnityEngine;

namespace Movement
{
    [RequireComponent(typeof(Rigidbody))]
    internal class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private MovementView _movementView;

        private const float Speed = 2.4f;
        private const float MaxAngleRotation = 360;
        private const float AngleRotation = 90;
        private const float SpeedAnimationRotate = 0.5f;
        private const float LerpMaterial = 8;

        private Rigidbody _rigidbody;
        private Material _material;
        private Vector3 _moveDirection;
        private float _nextRotateY;
        private float _speedMaterial;

        public bool IsReady { get; private set; }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _material = _movementView.GetComponent<Renderer>().material;
            _speedMaterial = Speed / LerpMaterial;
        }

        internal void Init()
        {
            _nextRotateY = transform.eulerAngles.y;
            UpdateDirection();
        }

        internal void Rotate()
        {
            IsReady = false;
            _nextRotateY = transform.eulerAngles.y + AngleRotation;
            transform.LeanRotateY(_nextRotateY, SpeedAnimationRotate).setOnComplete(UpdateDirection);
        }

        internal void Scroll()
        {
            Vector3 nextPosition = _rigidbody.position;
            
            _rigidbody.position += _moveDirection * Speed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(nextPosition);

            _material.mainTextureOffset += Vector2.up * _speedMaterial * Speed * Time.fixedDeltaTime;
        }

        private void UpdateDirection()
        {
            const float zeroAngle = 0;
            const float rightAngle = 90;
            const float unfoldedAngle = 180;
            const float convexAngle = 270;

            Vector3 currentRotate = transform.eulerAngles;
            currentRotate.y = _nextRotateY;

            if (currentRotate.y >= MaxAngleRotation)
                currentRotate.y = 0;

            transform.eulerAngles = currentRotate;

            switch (transform.eulerAngles.y)
            {
                case zeroAngle:
                    _moveDirection = Vector3.back;
                    break;
                case rightAngle:
                    _moveDirection = Vector3.left;
                    break;
                case unfoldedAngle:
                    _moveDirection = Vector3.forward;
                    break;
                case convexAngle:
                    _moveDirection = Vector3.right;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(transform.eulerAngles.y));
            }

            IsReady = true;
        }
    }
}