using System;
using Source.Game.Scripts.Spawn;
using UnityEngine;

namespace Source.Game.Scripts.Presenter
{
    public class WerehousePresenter : MonoBehaviour
    {
        [SerializeField] private Transform _startDeliverPoint;
        [SerializeField] private Transform _loadingArea;
        [SerializeField] private ParticleSystem _smokeExplosion;

        private SpawnerBox _spawnerBox;
        private SceneLoader _sceneLoader;
        private TruckPresenter _truckPresenter;
        private int _currentMissBox;

        public event Action BoxFallen;

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out BoxPresenter boxPresenter))
            {
                BoxFallen?.Invoke();
                boxPresenter.PlayBadParticle();
                boxPresenter.PlayAudioComplete();
            }
        }

        public void Reset()
        {
            _smokeExplosion.Play();
            _sceneLoader.Load();
            _spawnerBox.Reset();
            _truckPresenter.transform.position = _startDeliverPoint.position;
            _truckPresenter.Reset();
            _truckPresenter.Move(_loadingArea.position);
        }

        public void Init(SpawnerBox spawnerBox, SceneLoader sceneLoader, TruckPresenter truckPresenter)
        {
            _spawnerBox = spawnerBox;
            _sceneLoader = sceneLoader;
            _truckPresenter = truckPresenter;
        }
    }
}