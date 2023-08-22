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

    public void Reset()
    {
        foreach (var heart in _hearts)
        {
            heart.gameObject.SetActive(true);
            _numberHeart = _hearts.Length;
        }

        _bordResurrect.gameObject.SetActive(false);
        _bordSkip.gameObject.SetActive(false);
    }

    public void Init()
    {
        _hearts = GetComponentsInChildren<HeartPresenter>();
        _numberHeart = _hearts.Where(heart => heart.gameObject.activeSelf == true).Count();
        _bordResurrect.gameObject.SetActive(false);
        _bordSkip.gameObject.SetActive(false);
    }

    public void TakeDamage()
    {
        HeartPresenter lastHeart = _hearts.LastOrDefault(heart => heart.gameObject.activeSelf == true);

        if (lastHeart != null)
        {
            lastHeart.gameObject.SetActive(false);
            _numberHeart--;
        }

        if (_numberHeart <= 0 && _bordResurrect.gameObject.activeSelf == false)
        {
            _spawnerBox.Inactive();

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
        const float Delay = 3;

        _bordResurrect.gameObject.SetActive(true);

        yield return new WaitForSeconds(Delay);

        _bordSkip.gameObject.SetActive(true);
    }
}