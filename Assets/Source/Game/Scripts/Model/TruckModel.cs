using System;
using Source.Game.Scripts.Presenter;
using UnityEngine;

namespace Source.Game.Scripts.Model
{
    public class TruckModel
    {
        private ParticleSystem[] _smokesExhaust;

        private TruckPresenter _presenter;

        private int _boxInBody;
        private bool _isDelivery;

        public event Action<int> AddScoreBody;
        public event Action LevelCompleted;

        public TruckModel(TruckPresenter presenter) =>
            _presenter = presenter;

        public void Init(ParticleSystem[] smokesExhaust, DeliverPoint endDeliverPoint, Config.Config config)
        {
            _smokesExhaust = smokesExhaust;
        }

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