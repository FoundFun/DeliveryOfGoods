using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoxPresenter : MonoBehaviour
{
    [SerializeField] private ParticleSystem _badParticle;
    [SerializeField] private ParticleSystem _goodParticle;

    private const float SpeedCleanAnimation = 1;

    private Rigidbody _rigidbody;
    private Coroutine _coroutine;

    public Vector3 TargetPosition { get; private set; }

    public void Init()
    {
        _rigidbody = GetComponent<Rigidbody>();
        TargetPosition = transform.position;
    }

    public void Reset()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        gameObject.SetActive(false);
        _rigidbody.velocity = Vector3.zero;
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
        const float Delay = 1;

        typeParticle.Play();

        yield return new WaitForSeconds(Delay);

        Vector3 templateScale = transform.localScale;

        transform.LeanScale(Vector3.zero, SpeedCleanAnimation);

        yield return new WaitForSeconds(Delay);

        Reset();
        transform.localScale = templateScale;
    }
}