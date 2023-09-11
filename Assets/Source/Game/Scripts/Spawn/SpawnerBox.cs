using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using DeliveryOfGoods.Model;

public class SpawnerBox : ObjectPool<BoxPresenter>
{
    [SerializeField] private BoxPresenter[] _boxs;
    [SerializeField] private GameObject _container;
    [SerializeField] private Config _config;

    private List<BoxPresenter> _boxes;
    private SpawnPoint _spawnPoints;
    private Coroutine _spawnCoroutine;
    private int _boxIndex;
    private bool _isGenerate;

    private void Awake()
    {
        _spawnPoints = GetComponentInChildren<SpawnPoint>();
        _boxes = Initialize(_boxs, _container);
        Init();
    }

    public void Reset()
    {
        foreach(BoxPresenter box in _boxes)
            box.Reset();
    }

    public void Active()
    {
        _isGenerate = true;

        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);

        _spawnCoroutine = StartCoroutine(Generate());
    }

    public void Inactive()
    {
        _isGenerate = false;

        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);
    }
    
    private void Init()
    {
        foreach (var box in _boxes)
            box.Init();
    }

    private IEnumerator Generate()
    {
        while (_isGenerate)
        {
            if (TryGetObject(out BoxPresenter box, _boxIndex))
            {
                box.transform.position = _spawnPoints.transform.position;
                box.Activate();

                yield return new WaitForSeconds(_config.SpawnSpeed);
            }

            _boxIndex++;

            yield return null;
        }
    }
}