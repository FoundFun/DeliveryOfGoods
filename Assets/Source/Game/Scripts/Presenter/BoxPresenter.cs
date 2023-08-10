using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoxPresenter : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private bool _isBuy;

    private const float SpeedCleanAnimation = 1;

    private Rigidbody _rigidbody;
    private Coroutine _coroutine;

    public int Price => _price;

    public bool IsBuy => _isBuy;

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

    public void Buy()
    {
        _isBuy = true;
    }

    public void Clean()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(OnClean());
    }

    private IEnumerator OnClean()
    {
        const float delayClean = 1;

        yield return new WaitForSeconds(delayClean);

        Vector3 templateScale = transform.localScale;

        transform.LeanScale(Vector3.zero, SpeedCleanAnimation);

        yield return new WaitForSeconds(delayClean);

        Reset();
        transform.localScale = templateScale;
    }
}