using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using DeliveryOfGoods.Model;

public class SpawnerBox : ObjectPool<BoxPresenter>
{
    [SerializeField] private BoxPresenter[] _boxs;
    [SerializeField] private GameObject _container;

    private List<BoxPresenter> _boxes;
    private List<BoxPresenter> _purchasedBoxes;
    private SpawnPoint _spawnPoints;
    private Coroutine _spawnCoroutine;
    private int _boxIndex;

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
        UpdatePurchasedBoxs();

        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);

        _spawnCoroutine = StartCoroutine(Generate());
    }

    public void Inactive()
    {
        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);
    }

    public void UpdatePurchasedBoxs()
    {
        _purchasedBoxes = _boxes.Take(Config.MaxNumberBoxs).ToList();
    }

    private IEnumerator Generate()
    {
        while (true)
        {
            if (TryGetObject(out BoxPresenter box, _boxIndex))
            {
                box.transform.position = _spawnPoints.transform.position;
                box.gameObject.SetActive(true);

                yield return new WaitForSeconds(Config.SpawnSpeed);
            }

            _boxIndex++;

            if (_boxIndex >= _purchasedBoxes.Count)
                _boxIndex = 0;

            yield return null;
        }
    }

    private void Init()
    {
        foreach (var box in _boxes)
            box.Init();
    }
}