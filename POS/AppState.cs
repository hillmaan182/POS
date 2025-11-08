
namespace POS
{
    public class AppState
    {
        public event Action OnChange;
        private bool _openDrawer;
        public bool openDrawer
        {
            get => _openDrawer;
            set
            {
                if (_openDrawer != value)
                {
                    _openDrawer = value;
                    NotifyStateChanged();
                }
                else
                {
                    _openDrawer = value;
                    NotifyStateChanged();
                }
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
