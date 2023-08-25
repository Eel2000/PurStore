namespace PureStore.App
{
    public partial class App : IApplication
    {
        public App()
        {
            InitializeComponent();

#if WINDOWS
            MainPage = new DesktopShell();
#else
            MainPage = new AppShell();
#endif
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            window.MinimumHeight = 820;
            window.MinimumWidth = 1220;
            window.Title = "All Store App";

            return window;
        }

        protected override void OnStart()
        {
            base.OnStart();
            UserAppTheme = AppTheme.Dark;
        }

        protected override void OnResume()
        {
            base.OnResume();
            UserAppTheme = AppTheme.Dark;
        }
    }
}