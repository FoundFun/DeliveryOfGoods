using System;
using UnityEngine;

namespace Source.Game.Scripts.Model
{
    public class TruckModel
    {
        private ParticleSystem[] _smokesExhaust;

        private int _boxInBody;
        private bool _isDelivery;

        public event Action<int> AddScoreBody;
        public event Action LevelCompleted;

        public void Init(ParticleSystem[] smokesExhaust) => 
            _smokesExhaust = smokesExhaust;

        public void Reset()
        {
            _isDelivery = false;
            _boxInBody = 0;
            AddScoreBody?.Invoke(_boxInBody);
        }

        public void Move(Transform transform, Vector3 targetPosition)
        {
            const float animationTime = 3;

            PlayExhaust();
            transform.LeanMoveZ(targetPosition.z, animationTime).setOnComplete(StopExhaust);
        }

        public void Deliver(Transform transform, Vector3 targetPosition)
        {
            const float animationTime = 3;

            _isDelivery = true;
            PlayExhaust();
            transform.LeanMoveZ(targetPosition.z, animationTime);
            Complete();
        }

        public void AddScore(int score)
        {
            _boxInBody = score;
            AddScoreBody?.Invoke(_boxInBody);
        }

        private void Complete() =>
            LevelCompleted?.Invoke();

        private void PlayExhaust()
        {
            foreach (ParticleSystem smoke in _smokesExhaust)
                smoke.Play();
        }

        private void StopExhaust()
        {
            foreach (ParticleSystem smoke in _smokesExhaust)
                smoke.Stop();
        }
    }
}