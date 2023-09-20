using System;
using System.Collections;
using Source.Game.Scripts.Presenter;
using UnityEngine;

namespace Source.Game.Scripts.Model
{
    public class BoxModel
    {
        private readonly BoxPresenter _presenter;

        private Rigidbody _rigidbody;
        private Coroutine _coroutineParticle;
        private Coroutine _coroutineExplosion;
        private ParticleSystem _explosion;
        private ParticleSystem _goodParticle;
        private ParticleSystem _badParticle;
        private AudioSource _pushSound;
        private AudioSource _completeSound;

        public BoxModel(BoxPresenter presenter) => 
            _presenter = presenter;

        public void Reset()
        {
            if (_coroutineParticle != null)
                _presenter.StopCoroutine(_coroutineParticle);

            if (_coroutineExplosion != null)
                _presenter.StopCoroutine(_coroutineExplosion);

            if (_presenter.gameObject.activeSelf)
                _coroutineExplosion = _presenter.StartCoroutine(Deactivate());
        }

        public void Init(Rigidbody rigidbody, ParticleSystem badParticle, ParticleSystem goodParticle,
            ParticleSystem explosion, AudioSource completeSound, AudioSource pushSound)
        {
            _rigidbody = rigidbody;
            _rigidbody = rigidbody;
            _explosion = explosion;
            _goodParticle = goodParticle;
            _badParticle = badParticle;
            _completeSound = completeSound;
            _pushSound = pushSound;
        }

        public void Activate()
        {
            _explosion.Play();
            _presenter.gameObject.SetActive(true);
            _pushSound.Play();
        }

        public void PlayAudioComplete()
        {
            if (_presenter.gameObject.activeSelf)
                _completeSound.Play();
        }

        public void PlayGoodParticle()
        {
            _goodParticle.Play();
        }

        public void PlayBadParticle()
        {
            if (!_presenter.gameObject.activeSelf)
                return;
            
            if (_coroutineParticle != null)
                _presenter.StopCoroutine(_coroutineParticle);

            _coroutineParticle = _presenter.StartCoroutine(OnPlayBadParticle(_badParticle));
        }

        private IEnumerator OnPlayBadParticle(ParticleSystem typeParticle)
        {
            const float delay = 1;

            typeParticle.Play();

            yield return new WaitForSeconds(delay);

            Reset();
        }

        private IEnumerator Deactivate()
        {
            const float speedCleanAnimation = 1;

            _explosion.Play();

            Vector3 templateScale = _presenter.transform.localScale;

            _presenter.transform.LeanScale(Vector3.zero, speedCleanAnimation);

            yield return new WaitWhile(() => _explosion.isPlaying);

            _presenter.gameObject.transform.localScale = templateScale;
            _rigidbody.velocity = Vector3.zero;
            _presenter.gameObject.SetActive(false);
        }
    }
}