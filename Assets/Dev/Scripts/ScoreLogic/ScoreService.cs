namespace Dev.ScoreLogic
{
    public class ScoreService
    {
        private ScoreView _scoreView;

        private int _score = 0;

        public int Score => _score;

        public ScoreService(ScoreView scoreView)
        {
            _scoreView = scoreView;
        }

        public void AddScore(int scoreAmount)
        {
            _score += scoreAmount;
            UpdateView();
        }

        public void ResetScore()
        {
            _score = 0;
            UpdateView();
        }

        private void UpdateView()
        {
            _scoreView.UpdateScore(_score);
        }
    }
}