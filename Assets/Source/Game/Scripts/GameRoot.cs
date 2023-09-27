using Source.Game.Scripts.Configure;
using Source.Game.Scripts.Model;
using Source.Game.Scripts.Presenter;
using Source.Game.Scripts.Presenter.UI;
using Source.Game.Scripts.Spawn;
using Source.Game.Scripts.Yandex;
using UnityEngine;

namespace Source.Game.Scripts
{
    public class GameRoot : MonoBehaviour
    {
        [SerializeField] private Config _config;
        [SerializeField] private SpawnerBox _spawnerBox;
        [SerializeField] private MenuGamePresenter _menuGamePresenter;
        [SerializeField] private GamePresenter _gamePresenter;
        [SerializeField] private TruckPresenter _truckPresenter;
        [SerializeField] private BordSkipPresenter _bordSkipPresenter;
        [SerializeField] private BordHeartPresenter _bordHeartPresenter;
        [SerializeField] private BordResurrectPresenter _bordResurrectPresenter;
        [SerializeField] private WarehousePresenter _warehousePresenter;
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private YandexShowAds _yandexShowAds;

        private TruckModel _truckModel;

        private void Awake()
        {
            _truckModel = new TruckModel();

            _sceneLoader.Init(_config);
            _truckPresenter.Init(_truckModel, _config);
            _spawnerBox.Init(_config);
            _warehousePresenter.Init(_spawnerBox,
                _sceneLoader, _truckPresenter);
            _bordHeartPresenter.Init(_spawnerBox,
                _bordResurrectPresenter, _bordSkipPresenter, _config);
            _bordSkipPresenter.Init(_bordHeartPresenter, _yandexShowAds);
            _bordResurrectPresenter.Init(_bordHeartPresenter,
                _yandexShowAds);
            _gamePresenter.Init(_config, _bordHeartPresenter,
                _spawnerBox, _sceneLoader, _yandexShowAds);
            _menuGamePresenter.Init(_config);

            _menuGamePresenter.Open();
            _gamePresenter.Close();
        }

        private void Start()
        {
            _config.UpdateValue();
            _gamePresenter.EnableStartTutorial();
            _sceneLoader.Load();
            _warehousePresenter.Reset();
        }

        private void OnEnable()
        {
            _config.ChangedTargetScore += _gamePresenter.SetTargetScore;
            _menuGamePresenter.OpenedGame += _gamePresenter.Open;
            _gamePresenter.OpenedMenu += _menuGamePresenter.Open;
            _gamePresenter.ResetScene += _warehousePresenter.Reset;
            _gamePresenter.ResetHeart += _bordHeartPresenter.Reset;
            _gamePresenter.LoadedNextScene += _warehousePresenter.Reset;
            _bordSkipPresenter.Restart += _warehousePresenter.Reset;
            _truckModel.AddScoreBody += _gamePresenter.OnAddScore;
            _truckModel.LevelCompleted += _gamePresenter.OnLevelCompleted;
            _warehousePresenter.BoxFallen += _gamePresenter.OnBoxFallen;
        }

        private void OnDisable()
        {
            _config.ChangedTargetScore -= _gamePresenter.SetTargetScore;
            _menuGamePresenter.OpenedGame -= _gamePresenter.Open;
            _gamePresenter.OpenedMenu -= _menuGamePresenter.Open;
            _gamePresenter.ResetScene -= _warehousePresenter.Reset;
            _gamePresenter.ResetHeart -= _bordHeartPresenter.Reset;
            _gamePresenter.LoadedNextScene -= _warehousePresenter.Reset;
            _bordSkipPresenter.Restart -= _warehousePresenter.Reset;
            _truckModel.AddScoreBody -= _gamePresenter.OnAddScore;
            _truckModel.LevelCompleted -= _gamePresenter.OnLevelCompleted;
            _warehousePresenter.BoxFallen -= _gamePresenter.OnBoxFallen;
        }
    }
}