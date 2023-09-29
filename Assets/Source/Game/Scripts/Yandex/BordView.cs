using TMPro;
using UnityEngine;

namespace Source.Game.Scripts.Yandex
{
    public class BordView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _number;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _score;

        public void SetValue(int number, string name, int score)
        {
            _number.text = number.ToString();
            _name.text = name;
            _score.text = score.ToString();
        }
    }
}