using System;
using UnityEngine;

namespace DeliveryOfGoods.Model
{
    public abstract class Rotatable
    {
        private const float MaxAngleRotation = 360;
        private const float AngleRotation = 90;

        private float _currentRotateY;
        private float _nextRotateY;

        public Vector3 MoveDirection { get; private set; }

        public void Init(float roadEulerAnglesY)
        {
            _currentRotateY = roadEulerAnglesY;
            UpdateDirection();
        }

        public void Rotate(Transform road)
        {
            _nextRotateY = _currentRotateY + AngleRotation;
            road.LeanRotateY(_nextRotateY, 2).setOnComplete(UpdateDirection);

            _currentRotateY = _nextRotateY;

            if (_currentRotateY >= MaxAngleRotation)
                _currentRotateY = 0;
        }

        private void UpdateDirection()
        {
            const float ZeroAngle = 0;
            const float RightAngle = 90;
            const float UnfoldedAngle = 180;
            const float ConvexAngle = 270;

            switch (_currentRotateY)
            {
                case ZeroAngle:
                    MoveDirection = Vector3.back;
                    break;
                case RightAngle:
                    MoveDirection = Vector3.left;
                    break;
                case UnfoldedAngle:
                    MoveDirection = Vector3.forward;
                    break;
                case ConvexAngle:
                    MoveDirection = Vector3.left;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_currentRotateY));
            }
        }
    }
}