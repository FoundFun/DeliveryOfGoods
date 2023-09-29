using Source.Game.Scripts.View;
using UnityEngine;

namespace Source.Game.Scripts.Presenter.UI
{
    public class ScorePresenter : MonoBehaviour
    {
        [SerializeField] private ScoreView _view;

        public void Init() => 
            _view.Init();

        public void SetTargetScore(int score) => 
            _view.SetTargetScore(score);

        public void AddScore(int score) => 
            _view.AddScore(score);
    }
}