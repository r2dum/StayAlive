public class CurrentScorePresenter
{
    private CurrentScore _model;
    private CurrentScoreView _view;

    public CurrentScorePresenter(CurrentScore model, CurrentScoreView view)
    {
        _model = model;
        _view = view;
    }
    
    public void Enable()
    {
        Bomb.Dropped += OnScoreChanged;
        _model.ScoreChanged += UpdateView;
    }

    public void Disable()
    {
        Bomb.Dropped -= OnScoreChanged;
        _model.ScoreChanged -= UpdateView;
    }

    private void OnScoreChanged()
    {
        _model.AddScore();
    }

    private void UpdateView(int score)
    {
        _view.SetView(score);
    }
}
