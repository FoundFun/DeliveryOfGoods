using System.Collections;
using UnityEngine;

namespace Source.Game.Scripts.Model
{
    public class BoxModel
    {
        private Rigidbody _rigidbody;
        private Coroutine _coroutineParticle;
        private Coroutine _coroutineExplosion;
        private ParticleSystem _explosion;
        private ParticleSystem _goodParticle;
        private ParticleSystem _badParticle;
        private AudioSource _pushSound;
        private AudioSource _completeSound;

        public void Reset(MonoBehaviour objectBehaviour)
        {
            if (_coroutineParticle != null)
                objectBehaviour.StopCoroutine(_coroutineParticle);

            if (_coroutineExplosion != null)
                objectBehaviour.StopCoroutine(_coroutineExplosion);

            if (objectBehaviour.gameObject.activeSelf)
                _coroutineExplosion = objectBehaviour.StartCoroutine(Deactivate(objectBehaviour.gameObject));
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

        public void Activate(GameObject gameObject)
        {
            _explosion.Play();
            gameObject.SetActive(true);
            _pushSound.Play();
        }

        public void PlayAudioComplete(GameObject gameObject)
        {
            if (gameObject.activeSelf)
                _completeSound.Play();
        }

        public void PlayGoodParticle() => 
            _goodParticle.Play();

        public void PlayBadParticle(MonoBehaviour objectBehaviour)
        {
            if (!objectBehaviour.gameObject.activeSelf)
                return;

            if (_coroutineParticle != null)
                objectBehaviour.StopCoroutine(_coroutineParticle);

            _coroutineParticle = objectBehaviour.StartCoroutine(OnPlayBadParticle(objectBehaviour, _badParticle));
        }

        private IEnumerator OnPlayBadParticle(MonoBehaviour objectBehaviour, ParticleSystem typeParticle)
        {
            const float delay = 1;

            typeParticle.Play();

            yield return new WaitForSeconds(delay);

            Reset(objectBehaviour);
        }

        private IEnumerator Deactivate(GameObject gameObject)
        {
            const float speedCleanAnimation = 0.2f;

            _explosion.Play();

            Vector3 templateScale = gameObject.transform.localScale;

            gameObject.transform.LeanScale(Vector3.zero, speedCleanAnimation);

            yield return new WaitForSeconds(speedCleanAnimation);

            gameObject.gameObject.transform.localScale = templateScale;
            _rigidbody.velocity = Vector3.zero;
            gameObject.gameObject.SetActive(false);
        }
    }
}