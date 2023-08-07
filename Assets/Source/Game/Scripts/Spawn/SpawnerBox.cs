using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class SpawnerBox : ObjectPool<BoxPresenter>
{
    [SerializeField] private BoxPresenter[] _boxs;
    [SerializeField] private GameObject _container;

    private readonly WaitForSeconds _spawnTime = new WaitForSeconds(3);

    private List<BoxPresenter> _boxes;
    private SpawnPoint _spawnPoints;
    private Coroutine _spawnCoroutine;

    private void Awake()
    {
        _spawnPoints = GetComponentInChildren<SpawnPoint>();
        _boxes = Initialize(_boxs, _container);
        Init();
    }

    private void Start()
    {
        UpdateBoxs();

        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);

        _spawnCoroutine = StartCoroutine(Generate());
    }

    public void UpdateBoxs()
    {
        BoxPresenter[] boxs = _boxs.Where(box => box.IsBuy == true).ToArray();
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
        foreach (var box in _boxes)
            box.Init();
    }
}