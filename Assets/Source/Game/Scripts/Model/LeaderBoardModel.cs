using Source.Game.Scripts.View;

namespace Source.Game.Scripts.Model
{
    public class LeaderBoardModel
    {
        private readonly LeaderBoardView _view;
        private readonly int _number;
        private readonly string _name;
        private readonly int _score;

        public LeaderBoardModel(LeaderBoardView view,int number, int score, string name)
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