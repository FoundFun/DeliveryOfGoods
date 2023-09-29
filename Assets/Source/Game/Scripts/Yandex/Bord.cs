namespace Source.Game.Scripts.Yandex
{
    public class Bord
    {
        private readonly BordView _view;
        private readonly int _number;
        private readonly string _name;
        private readonly int _score;

        public Bord(BordView view,int number, string name, int score)
        {
            _view = view;
            _number = number;
            _name = name;
            _score = score;
        }

        public void SetValue() => 
            _view.SetValue(_number, _name, _score);
    }
}