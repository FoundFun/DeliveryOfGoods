using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoxPresenter : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private const float SpeedCleanAnimation = 1;
    private const float MoveParticleAnimation = 2;

    private Rigidbody _rigidbody;
    private Coroutine _coroutine;

    public Vector3 TargetPosition { get; private set; }

    public void Init()
    {
        TargetPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Reset()
    {
        gameObject.SetActive(false);
        _rigidbody.velocity = Vector3.zero;
    }

    public void Complete()
    {
        Debug.Log("+1");
    }

    public void Clean()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(OnClean());
    }

    private IEnumerator OnClean()
    {
        const float Delay = 1;

        _particleSystem.Play();

        yield return new WaitForSeconds(Delay);

        Vector3 templateScale = transform.localScale;

        transform.LeanScale(Vector3.zero, SpeedCleanAnimation);

        yield return new WaitForSeconds(Delay);

        Reset();
        transform.localScale = templateScale;
    }
}