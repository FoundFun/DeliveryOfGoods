using System;
using UnityEngine;

namespace Source.Game.Scripts.Model
{
    public class TruckModel
    {
        private ParticleSystem[] _smokesExhaust;

        public int BoxInBody { get; private set; }

        public bool IsDelivery { get; private set; }

        public event Action<int> AddScoreBody;
        public event Action LevelCompleted;

        public void Init(ParticleSystem[] smokesExhaust) => 
            _smokesExhaust = smokesExhaust;

        public void Reset()
        {
            IsDelivery = false;
            BoxInBody = 0;
            AddScoreBody?.Invoke(BoxInBody);
        }

        public void Move(Transform transform, Vector3 targetPosition)
        {
            const float animationTime = 3;

            PlayExhaust();
            transform.LeanMoveZ(targetPosition.z, animationTime).setOnComplete(StopExhaust);
        }

        public void Deliver(Transform transform, Vector3 targetPosition)
        {
            const float animationTime = 2;

            IsDelivery = true;
            PlayExhaust();
            transform.LeanMoveZ(targetPosition.z, animationTime);
            Complete();
        }

        public void AddScore()
        {
            BoxInBody++;
            AddScoreBody?.Invoke(BoxInBody);
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