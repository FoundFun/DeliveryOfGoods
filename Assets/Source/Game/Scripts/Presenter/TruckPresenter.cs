using System;
using Source.Game.Scripts.Model;
using UnityEngine;

namespace Source.Game.Scripts.Presenter
{
    public class TruckPresenter : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] _smokesExhaust;
        [SerializeField] private DeliverPoint _endDeliverPoint;
        [SerializeField] private Config.Config _config;

        private TruckModel _model;

        private int _boxInBody;
        private bool _isDelivery;

        public event Action<int> AddScoreBody;
        public event Action LevelCompleted;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out BoxPresenter box))
                return;

            box.PlayGoodParticle();
            box.PlayAudioComplete();
            _boxInBody++;
            AddScoreBody?.Invoke(_boxInBody);

            if (_boxInBody >= _config.CurrentDeliverBox && _isDelivery == false)
                _model.Deliver(gameObject.transform, _endDeliverPoint.transform.position);
        }

        public void Init(TruckModel model)
        {
            _model = model;
            _model.Init(_smokesExhaust, _endDeliverPoint, _config);
        }

        public void Reset()
        {
            _isDelivery = false;
            _boxInBody = 0;
            AddScoreBody?.Invoke(_boxInBody);
        }

        private void Complete()
        {
            LevelCompleted?.Invoke();
        }

        public void Move(Vector3 loadingAreaPosition) => 
            _model.Move(gameObject.transform, loadingAreaPosition);
    }
}