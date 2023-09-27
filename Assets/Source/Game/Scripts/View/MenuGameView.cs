using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Game.Scripts.View
{
    public class MenuGameView : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private TMP_Text _startText;

        private Coroutine _coroutine;
        private bool _isMenuGame;

        public event Action StartButtonClick;

        private void OnEnable() =>
            _startButton.onClick.AddListener(OnStartButtonClick);

        private void OnDisable() =>
            _startButton.onClick.RemoveListener(OnStartButtonClick);

        private void OnStartButtonClick() =>
            StartButtonClick?.Invoke();

        public void StartAnimationText()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _isMenuGame = true;
            _coroutine = StartCoroutine(PlayAnimationText());
        }

        public void StopAnimationText()
        {
            _isMenuGame = false;
            StopCoroutine(_coroutine);
        }

        private IEnumerator PlayAnimationText()
        {
            const float time = 1;
            Vector3 startScale = new (0.6f, 0.6f, 0.6f);

            while (_isMenuGame)
            {
                _startText.gameObject.LeanScale(Vector3.one, time).setLoopPingPong();

                yield return new WaitForSeconds(time);

                _startText.gameObject.LeanScale(startScale, time).setLoopPingPong();
                
                yield return new WaitForSeconds(time);
            }
        }
    }
}