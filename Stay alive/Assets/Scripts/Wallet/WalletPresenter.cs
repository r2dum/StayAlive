public class WalletPresenter
{
    private WalletSoundWithUIView _view;
    private Wallet _model;

    public WalletPresenter(WalletSoundWithUIView view, Wallet model)
    {
        _view = view;
        _model = model;
    }

    public void Enable()
    {
        _model.CoinsChanged += ViewUpdate;
    }

    public void Disable()
    {
        _model.CoinsChanged -= ViewUpdate;
    }
    
    public void ViewUpdate()
    {
        _view.SetAmount(_model.Coins);
    }
}
