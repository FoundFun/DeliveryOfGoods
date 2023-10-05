using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Game.Scripts.View
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _tutorialButton;
        [SerializeField] private Button _emptyTutorialButton;
        [SerializeField] private TMP_Text _tutorialText;

        private Coroutine _coroutine;
        private bool _isTutorial;

        public event Action ExitButtonClick;
        public event Action LoadNextLevel;

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(OnExitButtonClick);
            _nextLevelButton.onClick.AddListener(OnLoadNextLevel);
            _tutorialButton.onClick.AddListener(EnableTutorial);
            _emptyTutorialButton.onClick.AddListener(DisableTutorial);
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(OnExitButtonClick);
            _nextLevelButton.onClick.RemoveListener(OnLoadNextLevel);
            _tutorialButton.onClick.RemoveListener(EnableTutorial);
            _emptyTutorialButton.onClick.RemoveListener(DisableTutorial);
        }

        public void EnableNextLevelButton()
        {
            const float animationTime = 0.5f;

            _nextLevelButton.interactable = true;
            _nextLevelButton.transform.LeanScale(Vector3.one, animationTime).setEaseOutExpo();
        }

        public void DisableNextButton()
        {
            const float animationTime = 0.5f;

            _nextLevelButton.interactable = false;
            _nextLevelButton.transform.LeanScale(Vector3.zero, animationTime).setEaseOutExpo();
        }

        public void DisableTutorial()
        {
            const float animationTime = 0.2f;

            _tutorialText.transform.LeanScale(Vector3.zero, animationTime);

            _isTutorial = false;

            if (_emptyTutorialButton.gameObject.activeSelf)
                _emptyTutorialButton.gameObject.SetActive(false);
        }

        public void EnableTutorial()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            if (!_isTutorial)
                OnEnableTutorial();
            else
                DisableTutorial();
        }

        private void OnEnableTutorial()
        {
            const float animationTime = 0.2f;

            _isTutorial = true;
            _tutorialText.gameObject.LeanScale(Vector3.one, animationTime);
        }

        private void OnExitButtonClick() =>
            ExitButtonClick?.Invoke();

        private void OnLoadNextLevel() =>
            LoadNextLevel?.Invoke();
    }
}