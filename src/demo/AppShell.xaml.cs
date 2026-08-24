namespace ShadersCarouselDemo
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

#if ANDROID
            DrawnUi.Draw.Super.SetStatusBarColor(Android.Graphics.Color.ParseColor("#7B32AB"));
            DrawnUi.Draw.Super.SetNavigationBarColor(Color.FromArgb("#7B32AB"), Color.FromArgb("#7B32AB"), true);
#endif
        }
    }
}
