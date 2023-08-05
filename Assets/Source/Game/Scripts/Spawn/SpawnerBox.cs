using System.Collections;
using UnityEngine;
using System.Linq;

public class SpawnerBox : ObjectPool<BoxPresenter>
{
    [SerializeField] private BoxPresenter[] _boxs;
    [SerializeField] private GameObject _container;

    private readonly WaitForSeconds _spawnTime = new WaitForSeconds(3);

    private SpawnPoint _spawnPoints;
    private Coroutine _spawnCoroutine;

    private void Awake()
    {
        _spawnPoints = GetComponentInChildren<SpawnPoint>();
    }

    private void Start()
    {
        Init();

        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);

        _spawnCoroutine = StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        while (true)
        {
            if (TryGetObject(out BoxPresenter box))
            {
                box.transform.position = _spawnPoints.transform.position;
                box.gameObject.SetActive(true);
            }

            yield return _spawnTime;
        }
    }

    private void Init()
    {
        BoxPresenter[] boxs = _boxs.Where(box => box.IsBuy == true).ToArray();

        Initialize(boxs, _container);
    }
}