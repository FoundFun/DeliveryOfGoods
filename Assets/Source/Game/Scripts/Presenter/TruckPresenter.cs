using Source.Game.Scripts.Configure;
using Source.Game.Scripts.Model;
using Source.Game.Scripts.Yandex;
using UnityEngine;

namespace Source.Game.Scripts.Presenter
{
    public class TruckPresenter : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] _smokesExhaust;
        [SerializeField] private Transform _endDeliverPoint;

        private Config _config;
        private TruckModel _model;
        private YandexLeaderBoard _yandexLeaderBoard;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out BoxPresenter box))
                return;

            box.PlayGoodParticle();
            box.PlayAudioComplete();
            
            _model.AddScore();
            _config.AddScoreLeaderBord();
#if YANDEX_GAMES
            _yandexLeaderBoard.OnSetLeaderboardScoreButtonClick();
#endif            

            if (_model.BoxInBody >= _config.CurrentDeliverBox && _model.IsDelivery == false)
                _model.Deliver(gameObject.transform, _endDeliverPoint.position);
        }

        public void Reset() => 
            _model.Reset();

        public void Init(TruckModel model, Config config, YandexLeaderBoard yandexLeaderBoard)
        {
            _model = model;
            _config = config;
            _yandexLeaderBoard = yandexLeaderBoard;

            _model.Init(_smokesExhaust);
        }

        public void Move(Vector3 loadingAreaPosition) => 
            _model.Move(gameObject.transform, loadingAreaPosition);
    }
}