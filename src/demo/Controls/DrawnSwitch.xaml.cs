using DrawnUi.Draw;

namespace ShadersCarouselDemo.Controls;

public partial class DrawnSwitch : SkiaSwitch
{
    public DrawnSwitch()
    {
        InitializeComponent();
    }

    protected void OnTapped(object sender, ControlTappedEventArgs controlTappedEventArgs)
    {
        IsToggled = !IsToggled;
    }

}