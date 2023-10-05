using System.Collections;
using System.Linq;
using Source.Game.Scripts.Configure;
using Source.Game.Scripts.Spawn;
using UnityEngine;

namespace Source.Game.Scripts.Presenter.UI
{
    public class BoardHeartPresenter : MonoBehaviour
    {
        private SpawnerBox _spawnerBox;
        private BoardResurrectPresenter _boardResurrect;
        private BoardSkipPresenter _boardSkip;
        private HeartPresenter[] _hearts;
        private Coroutine _coroutine;
        private Config _config;
        private int _numberHeart;
        private bool _isLive;

        public void Reset()
        {
            if (!_isLive)
            {
                foreach (var heart in _hearts)
                    heart.ToFill();
            
                _numberHeart = _hearts.Length;
                _isLive = true;
            }
        
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        
            DisableBord();
        }

        public void Init(SpawnerBox spawnerBox, BoardResurrectPresenter boardResurrect,
            BoardSkipPresenter boardSkip, Config config)
        {
            _spawnerBox = spawnerBox;
            _boardResurrect = boardResurrect;
            _boardSkip = boardSkip;
            _config = config;

            _hearts = GetComponentsInChildren<HeartPresenter>();
            _numberHeart = _hearts.Count(heart => heart.Fill == 1);
            DisableBord();
            Reset();
        }

        public void TakeDamage()
        {
            HeartPresenter lastHeart = _hearts.LastOrDefault(heart => heart.Fill == 1);

            if (lastHeart != null)
            {
                _isLive = true;
                lastHeart.Empty();
                _numberHeart--;
            }

            if (_numberHeart > 0 || _isLive != true)
                return;
            
            _config.DisableGame();
            _isLive = false;
            _spawnerBox.Inactive();
            _spawnerBox.Reset();

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(EnableGameOverBord());
        }

        public void Recover()
        {
            Reset();
            _spawnerBox.Active();
            _config.EnableGame();
        }

        private IEnumerator EnableGameOverBord()
        {
            const float animationTime = 0.5f;
            const float delay = 2;

            _boardResurrect.ActiveRestartButton();
            _boardResurrect.transform.LeanScale(Vector3.one, animationTime).setEaseOutExpo();

            yield return new WaitForSeconds(delay);

            _boardSkip.ActiveRestartButton();
            _boardSkip.transform.LeanScale(Vector3.one, animationTime).setEaseOutExpo();
        }

        private void DisableBord()
        {
            _boardResurrect.transform.LeanScale(Vector3.zero, 0.1f).setEaseOutExpo();
            _boardSkip.transform.LeanScale(Vector3.zero, 0.1f).setEaseOutExpo();
        }
    }
}