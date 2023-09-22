using Source.Game.Scripts.Configure;
using Source.Game.Scripts.Model;
using UnityEngine;

namespace Source.Game.Scripts.Presenter
{
    public class TruckPresenter : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] _smokesExhaust;
        [SerializeField] private Transform _endDeliverPoint;

        private Config _config;
        private TruckModel _model;
        private int _boxInBody;
        private bool _isDelivery;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out BoxPresenter box))
                return;

            box.PlayGoodParticle();
            box.PlayAudioComplete();
            _boxInBody++;
            _model.AddScore(_boxInBody);

            if (_boxInBody >= _config.CurrentDeliverBox && _isDelivery == false)
                _model.Deliver(gameObject.transform, _endDeliverPoint.position);
        }

        public void Reset() => 
            _model.Reset();

        public void Init(TruckModel model, Config config)
        {
            _model = model;
            _config = config;

            _model.Init(_smokesExhaust);
        }

        public void Move(Vector3 loadingAreaPosition) => 
            _model.Move(gameObject.transform, loadingAreaPosition);
    }
}