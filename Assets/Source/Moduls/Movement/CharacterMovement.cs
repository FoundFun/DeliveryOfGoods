using DeliveryOfGoods.Model;
using System;
using UnityEngine;

namespace Movement
{
    [RequireComponent(typeof(Rigidbody))]
    internal class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private MovementView _movementView;
        [SerializeField] private float _speed;

        private const float MaxAngleRotation = 360;
        private const float AngleRotation = 90;
        private const float SpeedAnimationRotate = 0.5f;
        private const float LerpMaterial = 4;

        private Material _material;
        private Vector3 _moveDirection;
        private float _nextRotateY;
        private float _speedMaterial;

        public bool IsReady { get; private set; } = false;

        private void Awake()
        {
            _material = _movementView.GetComponent<Renderer>().material;
            _speedMaterial = Config.ConveyorSpeed / LerpMaterial;
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
            _rigidbody.position += _moveDirection * Config.ConveyorSpeed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(nextPosition);

            _material.mainTextureOffset += Vector2.up * _speedMaterial * Config.ConveyorSpeed * Time.fixedDeltaTime;
        }

        private void UpdateDirection()
        {
            const float ZeroAngle = 0;
            const float RightAngle = 90;
            const float UnfoldedAngle = 180;
            const float ConvexAngle = 270;

            Vector3 currentRotate;

            currentRotate = transform.eulerAngles;
            currentRotate.y = _nextRotateY;

            if (currentRotate.y >= MaxAngleRotation)
                currentRotate.y = 0;

            transform.eulerAngles = currentRotate;

            switch (transform.eulerAngles.y)
            {
                case ZeroAngle:
                    _moveDirection = Vector3.back;
                    break;
                case RightAngle:
                    _moveDirection = Vector3.left;
                    break;
                case UnfoldedAngle:
                    _moveDirection = Vector3.forward;
                    break;
                case ConvexAngle:
                    _moveDirection = Vector3.right;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(transform.eulerAngles.y));
            }

            IsReady = true;
        }
    }
}