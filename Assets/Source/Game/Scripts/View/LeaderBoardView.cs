using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Game.Scripts.View
{
    public class LeaderBoardView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _number;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private Image _badgeBest;

        private const string NullText = "";

        public void SetValue(int number, string publicName, int score)
        {
            _number.text = number.ToString();
            _name.text = publicName;
            _score.text = score.ToString();
            _badgeBest.enabled = true;
        }

        public void Clear()
        {
            _number.text = NullText;
            _name.text = NullText;
            _score.text = NullText;
            _badgeBest.enabled = false;
        }
    }
}