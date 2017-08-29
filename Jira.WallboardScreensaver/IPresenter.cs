namespace Jira.WallboardScreensaver {
    public interface IPresenter<in TView>
    {
        void Initialize(TView view);
    }
}
