using Source.Game.Scripts.Model;
using UnityEngine;

namespace Source.Game.Scripts.Presenter
{
    [RequireComponent(typeof(Rigidbody))]
    public class BoxPresenter : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _badParticle;
        [SerializeField] private ParticleSystem _goodParticle;
        [SerializeField] private ParticleSystem _explosion;
        [SerializeField] private AudioSource _completeSound;
        [SerializeField] private AudioSource _pushSound;

        private BoxModel _model;

        public void Reset() => 
            _model.Reset();

        public void Init(BoxModel model)
        {
            _model = model;
            
            _model.Init(GetComponent<Rigidbody>(), _badParticle, _goodParticle,
                _explosion, _completeSound, _pushSound);
        }

        public void Activate() => 
            _model.Activate();

        public void PlayAudioComplete() => 
            _model.PlayAudioComplete();

        public void PlayGoodParticle() => 
            _model.PlayGoodParticle();

        public void PlayBadParticle() => 
            _model.PlayBadParticle();
    }
}