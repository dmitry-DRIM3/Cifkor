public abstract class BasePresenter<TView> : IPresenter where TView : IView
{
    protected TView view;

    public BasePresenter(TView view)
    {
        this.view = view;
    }

    public abstract void Initialize();
    public abstract void Dispose();
}