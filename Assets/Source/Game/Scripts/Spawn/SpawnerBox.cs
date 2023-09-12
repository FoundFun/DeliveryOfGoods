using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using DeliveryOfGoods.Model;

public class SpawnerBox : ObjectPool<BoxPresenter>
{
    [SerializeField] private BoxPresenter[] _boxes;
    [SerializeField] private GameObject _container;
    [SerializeField] private Config _config;

    private List<BoxPresenter> _poolBoxes;
    private SpawnPoint _spawnPoints;
    private Coroutine _spawnCoroutine;
    private int _boxIndex;
    private bool _isGenerate;

    public void Reset()
    {
        foreach(BoxPresenter box in _poolBoxes)
            box.Reset();
    }
    
    public void Init()
    {
        _spawnPoints = GetComponentInChildren<SpawnPoint>();
        _poolBoxes = Initialize(_boxes, _container);
        
        foreach (var box in _poolBoxes)
            box.Init();
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

            if (_boxIndex >= _poolBoxes.Count)
                _boxIndex = 0;

            yield return null;
        }
    }
}