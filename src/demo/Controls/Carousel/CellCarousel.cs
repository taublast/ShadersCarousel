using System.Diagnostics;
using DrawnUi.Draw;
using ShadersCarousel.Models;
using SkiaSharp;

namespace ShadersCarouselDemo.Controls.Carousel;

public class CellCarousel : SkiaLayout
{
    public SimpleItemViewModel Model
    {
        get
        {
            return BindingContext as SimpleItemViewModel;
        }
    }

    private SkiaImage Banner;

    public CellCarousel()
    {
        //layout options will be set by parent carousel
        BackgroundColor = Colors.Black;
        UseCache = SkiaCacheType.Image;
        Children = new List<SkiaControl>()
        {
            //placeholder
            new SkiaSvg()
            {
                HeightRequest=110,
                HorizontalOptions = LayoutOptions.Center,
                LockRatio=1,
                TintColor=Color.Parse("#33CCCCCC"),
                UseCache = SkiaCacheType.Operations,
                VerticalOptions= LayoutOptions.Center,
                ZIndex = -1,
                SvgString = App.Current.Resources.Get<string>("SvgPlaceholder")
            },

            new SkiaImage()
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Aspect = TransformAspect.AspectCover,
                LoadSourceOnFirstDraw = false, //need to pre-create our cache for using in shader
                RescalingQuality = FilterQuality.Low,
                Tag="CellImage",
            }.Assign(out Banner)
        };

        this.WhenBindingContextSet((me, ctx) =>
        {
            if (ctx is SimpleItemViewModel model)
            {
                Banner.Source = model.Banner;
            }
        });
    }

 
}