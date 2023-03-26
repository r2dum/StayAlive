using UnityEngine;

public class CurrentScoreSetup : MonoBehaviour
{
    [SerializeField] private CurrentScoreView _currentScoreView;
    
    private CurrentScore _model;
    private CurrentScorePresenter _presenter;
    
    private void Awake()
    {
        _model = new CurrentScore();
        _presenter = new CurrentScorePresenter(_model, _currentScoreView);
    }

    private void OnEnable()
    {
        _presenter.Enable();
    }

    private void OnDisable()
    {
        _presenter.Disable();
    }
}
