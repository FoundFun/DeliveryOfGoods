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
    private Vector2 _startPosition;
    private int _numberHeart;

    public void Reset()
    {
        foreach (var heart in _hearts)
        {
            heart.ToFill();
            _numberHeart = _hearts.Length;
        }

        DisableBord();
    }

    public void Init()
    {
        _hearts = GetComponentsInChildren<HeartPresenter>();
        _numberHeart = _hearts.Where(heart => heart.Fill == 1).Count();
        DisableBord();
        Reset();
        _startPosition = transform.position;
    }

    public void TakeDamage()
    {
        HeartPresenter lastHeart = _hearts.LastOrDefault(heart => heart.Fill == 1);

        if (lastHeart != null)
        {
            lastHeart.Empty();
            _numberHeart--;
        }

        if (_numberHeart <= 0 || lastHeart == null)
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
        const float Delay = 2;

        _bordResurrect.transform.LeanMoveLocalY(_startPosition.y, 0.5f).setEaseOutExpo();

        yield return new WaitForSeconds(Delay);

        _bordSkip.transform.LeanMoveLocalY(_startPosition.y - 200, 0.5f).setEaseOutExpo();
    }

    private void DisableBord()
    {
        _bordResurrect.transform.LeanMoveLocalY(-Screen.height, 0.1f).setEaseOutExpo();
        _bordSkip.transform.LeanMoveLocalY(-Screen.height, 0.1f).setEaseOutExpo();
    }
}