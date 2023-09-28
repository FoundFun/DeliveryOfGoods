using System;
using System.Collections;
using Source.Game.Scripts.Yandex;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Game.Scripts.View
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private YandexLeaderBord _leaderBord;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _tutorialButton;
        [SerializeField] private Button _emptyTutorialButton;
        [SerializeField] private Button _openLeaderBordButton;
        [SerializeField] private Button _closeLeaderBordButton;
        [SerializeField] private TMP_Text _scoreInBodyText;
        [SerializeField] private TMP_Text _scoreTargetText;
        [SerializeField] private TMP_Text _tutorialText;

        private const string Slash = "/";

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
            _openLeaderBordButton.onClick.AddListener(OpenLeaderBord);
            _closeLeaderBordButton.onClick.AddListener(CloseLeaderBord);
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(OnExitButtonClick);
            _nextLevelButton.onClick.RemoveListener(OnLoadNextLevel);
            _tutorialButton.onClick.RemoveListener(EnableTutorial);
            _emptyTutorialButton.onClick.RemoveListener(DisableTutorial);
            _openLeaderBordButton.onClick.RemoveListener(OpenLeaderBord);
            _closeLeaderBordButton.onClick.RemoveListener(CloseLeaderBord);
        }

        public void Init() => 
            _scoreInBodyText.text = 0.ToString();

        public void AddScore(int score) => 
            _scoreInBodyText.text = score.ToString();

        public void SetTargetScore(int score) => 
            _scoreTargetText.text = Slash + score;

        public void EnableNextLevelButton()
        {
            const float animationTime = 0.5f;

            _nextLevelButton.transform.LeanScale(Vector3.one, animationTime).setEaseOutExpo();
        }

        public void DisableNextButton()
        {
            const float animationTime = 0.5f;

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
                _coroutine = StartCoroutine(OnEnableTutorial());
            else
                DisableTutorial();
        }

        private void OpenLeaderBord()
        {
            const float timeAnimation = 1f;

            _leaderBord.gameObject.LeanScale(Vector3.one, timeAnimation).setEaseOutElastic();
        }

        private void CloseLeaderBord()
        {
            const float timeAnimation = 0.5f;

            _leaderBord.gameObject.LeanScale(Vector3.zero, timeAnimation).setEaseInBack();
        }

        private IEnumerator OnEnableTutorial()
        {
            const float delay = 2;
            const float animationTime = 0.2f;

            _isTutorial = true;
            _tutorialText.gameObject.LeanScale(Vector3.one, animationTime);

            yield return new WaitForSeconds(delay);

            DisableTutorial();
        }

        private void OnExitButtonClick() => 
            ExitButtonClick?.Invoke();

        private void OnLoadNextLevel() => 
            LoadNextLevel?.Invoke();
    }
}