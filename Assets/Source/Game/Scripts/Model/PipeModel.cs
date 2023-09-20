using Source.Game.Scripts.Presenter;
using UnityEngine;

namespace Source.Game.Scripts.Model
{
    public class PipeModel
    {
        private const float SpeedRotation = 1.07f;
        private const float SpeedMaterial = 0.3f;
        private const float MultiplierSpeed = 400;

        private Rigidbody _rigidbody;
        private Material _material;

        private PipePresenter _presenter;

        public PipeModel(PipePresenter presenter)
        {
            _presenter = presenter;
        }

        public void Init(Rigidbody rigidbody, Material material)
        {
            _rigidbody = rigidbody;
            _material = material;
        }

        public void Rotate()
        {
            Quaternion rotation = _rigidbody.rotation;
            Quaternion move = Quaternion.Euler(0, MultiplierSpeed * SpeedRotation / (51 * Mathf.PI), 0);
            _rigidbody.MoveRotation(rotation * move);

            _material.mainTextureOffset += Vector2.right * SpeedMaterial * SpeedRotation * Time.fixedDeltaTime;
        }
    }
}