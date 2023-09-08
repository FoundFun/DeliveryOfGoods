using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class BoxPresenter : MonoBehaviour
{
    [SerializeField] private ParticleSystem _badParticle;
    [SerializeField] private ParticleSystem _goodParticle;
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _pushSound;

    private const float SpeedCleanAnimation = 1;

    private Rigidbody _rigidbody;
    private Coroutine _coroutineParticle;
    private Coroutine _coroutineExplosion;

    public void Reset()
    {
        if (_coroutineParticle != null)
            StopCoroutine(_coroutineParticle);
        
        if (_coroutineExplosion != null)
            StopCoroutine(_coroutineExplosion);
        
        if (gameObject.activeSelf == true)
            _coroutineExplosion = StartCoroutine(Deactivate());
    }

    public void Init()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    public void Activate()
    {        
        _explosion.Play();
        gameObject.SetActive(true);
        _pushSound.Play();
        _rigidbody.velocity = Vector3.zero;
    }

    public void PlayAudio()
    {
        if (gameObject.activeSelf == true)
            _audioSource.Play();
    }

    public void PlayGoodParticle()
    {
        _goodParticle.Play();
    }

    public void PlayBadParticle()
    {
        if (gameObject.activeSelf == true)
        {
            if (_coroutineParticle != null)
                StopCoroutine(_coroutineParticle);

            _coroutineParticle = StartCoroutine(OnPlayBadParticle(_badParticle));
        }
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
        _explosion.Play();
        
        Vector3 templateScale = transform.localScale;

        transform.LeanScale(Vector3.zero, SpeedCleanAnimation);
        
        yield return new WaitWhile(() => _explosion.isPlaying);
        
        gameObject.transform.localScale = templateScale;
        gameObject.SetActive(false);
    }
}