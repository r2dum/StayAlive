using UnityEngine;

public class WalletSetup : MonoBehaviour
{
    [SerializeField] private WalletSoundWithUIView _view;

    private Wallet _model;
    private WalletPresenter _presenter;

    private void Awake()
    {
        _model = new Wallet();
        _presenter = new WalletPresenter(_view, _model);
        _presenter.ViewUpdate();
    }

    private void OnEnable()
    {
        _presenter.Enable();
    }

    private void OnDisable()
    {
        _presenter.Disable();
    }

    public void AddCoin()
    {
        _model.AddCoin();
        _view.PlaySound();
    }
}
