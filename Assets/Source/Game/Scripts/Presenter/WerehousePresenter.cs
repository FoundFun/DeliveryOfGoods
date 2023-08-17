using DeliveryOfGoods.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WerehousePresenter : MonoBehaviour
{
    [SerializeField] private TruckPresenter _truckPresenter;
    [SerializeField] private Transform _startDeliverPoint;
    [SerializeField] private Transform _loadingArea;
    [SerializeField] private SpawnerBox _spawnerBox;

    private int _maxMissBox = 5;

    private int _currentMissBox;

    private void OnEnable()
    {
        _truckPresenter.SceneChanged += MoveTruck;
    }

    private void OnDisable()
    {
        _truckPresenter.SceneChanged -= MoveTruck;
    }

    private void Start()
    {
        SceneManager.LoadScene(Config.NameScene + Config.CurrentLevel);
        MoveTruck();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out BoxPresenter boxPresenter))
        {
            boxPresenter.Clean();
            _currentMissBox++;

            if (_currentMissBox >= _maxMissBox)
            {
                _truckPresenter.ResetScene();
                _currentMissBox = 0;
            }
        }
    }

    private void MoveTruck()
    {
        Debug.Log("PUSH");
        _spawnerBox.Reset();
        _truckPresenter.transform.position = _startDeliverPoint.position;
        _truckPresenter.Reset();
        _truckPresenter.Move(_loadingArea.position);
    }
}