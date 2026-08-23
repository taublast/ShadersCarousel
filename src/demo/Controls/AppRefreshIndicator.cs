using DrawnUi.Controls;
using DrawnUi.Draw;

namespace ShadersCarouselDemo.Controls;

public class AppRefreshIndicator : RefreshIndicator
{
    protected SkiaLottie Loader;
    public AppRefreshIndicator()
    {


    }

    public override void SetDragRatio(float ratio, float ptsScrollOffset, double ptsLimit, double ptsTrigger)
    {
        base.SetDragRatio(ratio, ptsScrollOffset, ptsLimit, ptsTrigger);

        if (FindLoader() && !IsRunning)
        {
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