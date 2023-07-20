namespace PureStore.App
{
    public partial class App : Application
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
    }
}