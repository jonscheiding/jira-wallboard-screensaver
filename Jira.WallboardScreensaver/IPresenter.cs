namespace Jira.WallboardScreensaver {
    public interface IPresenter<in TView> {
        void Initialize(TView view);
    }

    public interface IChildPresenter<in TView, in TParent> {
        void Initialize(TView view, TParent parent);
    }
}