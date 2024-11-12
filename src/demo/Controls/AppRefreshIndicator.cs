using DrawnUi.Maui.Controls;
using DrawnUi.Maui.Draw;

namespace ShadersCarouselDemo.Controls;

public class AppRefreshIndicator : RefreshIndicator
{
    protected SkiaLottie Loader;
    public AppRefreshIndicator()
    {


    }

    public override void SetDragRatio(float ratio)
    {
        base.SetDragRatio(ratio);

        if (FindLoader() && !IsRunning)
        {
            if (Loader.Animator == null)
                return; //failed to load source

            Loader.Seek(Loader.GetFrameAt(ratio));
        }
    }

    protected override void OnIsRunningChanged(bool value)
    {
        base.OnIsRunningChanged(value);

        if (FindLoader())
        {
            if (!value)
            {
                if (Loader.IsPlaying)
                    Loader.Stop();
            }
            else
            {
                if (!Loader.IsPlaying)
                    Loader.Start();
            }
        }
    }

    bool FindLoader()
    {
        if (Loader == null)
        {
            Loader = this.FindView<SkiaLottie>("Loader");
        }
        return Loader != null;
    }
}