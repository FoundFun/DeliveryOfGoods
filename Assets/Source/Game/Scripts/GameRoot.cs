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
        [SerializeField] private BoardSkipPresenter _boardSkipPresenter;
        [SerializeField] private BoardHeartPresenter _boardHeartPresenter;
        [SerializeField] private BoardResurrectPresenter _boardResurrectPresenter;
        [SerializeField] private ScorePresenter _scorePresenter;
        [SerializeField] private WarehousePresenter _warehousePresenter;
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private YandexShowAds _yandexShowAds;
        [SerializeField] private YandexLeaderBoard _yandexLeaderBoard;

        private TruckModel _truckModel;

        private void Awake()
        {
            _truckModel = new TruckModel();

            _sceneLoader.Init(_config);
            _truckPresenter.Init(_truckModel, _config, _yandexLeaderBoard);
            _spawnerBox.Init(_config);
            _warehousePresenter.Init(_spawnerBox,
                _sceneLoader, _truckPresenter);
            _boardHeartPresenter.Init(_spawnerBox,
                _boardResurrectPresenter, _boardSkipPresenter, _config);
            _boardSkipPresenter.Init(_boardHeartPresenter, _yandexShowAds);
            _boardResurrectPresenter.Init(_boardHeartPresenter,
                _yandexShowAds);
            _gamePresenter.Init(_config, _boardHeartPresenter,
                _spawnerBox, _sceneLoader, _yandexShowAds, _yandexLeaderBoard);
            _menuGamePresenter.Init(_config);
            _scorePresenter.Init();
            _yandexLeaderBoard.Init(_config,_spawnerBox);
            _yandexShowAds.Init(_boardResurrectPresenter);

            _menuGamePresenter.Open();
            _gamePresenter.Close();
        }

        private void Start()
        {
            _config.UpdateValue();
            _sceneLoader.Load();
            _warehousePresenter.Reset();
            _gamePresenter.EnableStartTutorial();
        }

        private void OnEnable()
        {
            _config.ChangedTargetScore += _gamePresenter.SetTargetScore;
            _menuGamePresenter.OpenedGame += _gamePresenter.Open;
            _gamePresenter.OpenedMenu += _menuGamePresenter.Open;
            _gamePresenter.ResetScene += _warehousePresenter.Reset;
            _gamePresenter.ResetHeart += _boardHeartPresenter.Reset;
            _gamePresenter.LoadedNextScene += _warehousePresenter.Reset;
            _boardSkipPresenter.Restart += _warehousePresenter.Reset;
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
            _gamePresenter.ResetHeart -= _boardHeartPresenter.Reset;
            _gamePresenter.LoadedNextScene -= _warehousePresenter.Reset;
            _boardSkipPresenter.Restart -= _warehousePresenter.Reset;
            _truckModel.AddScoreBody -= _gamePresenter.OnAddScore;
            _truckModel.LevelCompleted -= _gamePresenter.OnLevelCompleted;
            _warehousePresenter.BoxFallen -= _gamePresenter.OnBoxFallen;
        }
    }
}