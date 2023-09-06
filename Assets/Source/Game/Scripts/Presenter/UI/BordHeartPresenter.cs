using System.Collections;
using System.Linq;
using UnityEngine;

public class BordHeartPresenter : MonoBehaviour
{
    [SerializeField] private SpawnerBox _spawnerBox;
    [SerializeField] private BordResurrectPresenter _bordResurrect;
    [SerializeField] private BordSkipPresenter _bordSkip;

    private HeartPresenter[] _hearts;
    private Coroutine _coroutine;
    private int _numberHeart;
    private bool _isLive;

    public void Reset()
    {
        foreach (var heart in _hearts)
            heart.ToFill();

        _numberHeart = _hearts.Length;
        _isLive = true;
        DisableBord();
    }

    public void Init()
    {
        _hearts = GetComponentsInChildren<HeartPresenter>();
        _numberHeart = _hearts.Where(heart => heart.Fill == 1).Count();
        DisableBord();
        Reset();
    }

    public void TakeDamage()
    {
        HeartPresenter lastHeart = _hearts.LastOrDefault(heart => heart.Fill == 1);

        if (lastHeart != null)
        {
            _isLive = true;
            lastHeart.Empty();
            _numberHeart--;
        }

        if (_numberHeart <= 0 && _isLive == true)
        {
            _isLive = false;
            _spawnerBox.Inactive();
            _spawnerBox.Reset();

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(EnableGameOverBord());
        }
    }

    public void Recover()
    {
        Reset();
        _spawnerBox.Active();
    }

    private IEnumerator EnableGameOverBord()
    {
        const float animationTime = 0.5f;
        const float delay = 2;

        _bordResurrect.transform.LeanScale(Vector3.one, animationTime).setEaseOutExpo();

        yield return new WaitForSeconds(delay);

        _bordSkip.transform.LeanScale(Vector3.one, animationTime).setEaseOutExpo();
    }

    private void DisableBord()
    {
        _bordResurrect.transform.LeanScale(Vector3.zero, 0.1f).setEaseOutExpo();
        _bordSkip.transform.LeanScale(Vector3.zero, 0.1f).setEaseOutExpo();
    }
}