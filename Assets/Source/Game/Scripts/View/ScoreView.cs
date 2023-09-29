using TMPro;
using UnityEngine;

namespace Source.Game.Scripts.View
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreInBodyText;
        [SerializeField] private TMP_Text _scoreTargetText;
        
        private const string Slash = "/";

        public void Init() => 
            _scoreInBodyText.text = 0.ToString();

        public void SetTargetScore(int score) => 
            _scoreTargetText.text = Slash + score;

        public void AddScore(int score) => 
            _scoreInBodyText.text = score.ToString();
    }
}