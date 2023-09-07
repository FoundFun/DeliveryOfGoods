using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class BoxPresenter : MonoBehaviour
{
    [SerializeField] private ParticleSystem _badParticle;
    [SerializeField] private ParticleSystem _goodParticle;
    [SerializeField] private AudioSource _audioSource;

    private const float SpeedCleanAnimation = 1;

    private Rigidbody _rigidbody;
    private Coroutine _coroutine;

    public void Reset()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        gameObject.SetActive(false);
        _rigidbody.velocity = Vector3.zero;
    }

    public void Init()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(OnPlayBadParticle(_badParticle));
        }
    }

    private IEnumerator OnPlayBadParticle(ParticleSystem typeParticle)
    {
        const float delay = 1;

        typeParticle.Play();

        yield return new WaitForSeconds(delay);

        Vector3 templateScale = transform.localScale;

        transform.LeanScale(Vector3.zero, SpeedCleanAnimation);

        yield return new WaitForSeconds(delay);

        Reset();
        transform.localScale = templateScale;
    }
}