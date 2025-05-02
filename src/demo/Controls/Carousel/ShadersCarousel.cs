using DrawnUi.Controls;
using DrawnUi.Draw;
using ShadersCarousel.Models;
using SkiaSharp;
using System.Diagnostics;
using System.Numerics;

namespace ShadersCarouselDemo.Controls.Carousel;


/// <summary>
/// Sublclassed SkiaCarousel showing a shader effect for transitions
/// </summary>
public class ShadersCarousel : SkiaCarousel
{
    public ShadersCarousel()
    {
        RecyclingTemplate = RecyclingTemplate.Disabled; //todo make it work without this. We now use non-recycled to be able to attach existing cells that are same for every index at all times to our effect.

        Effect = new()
        {
            ShaderSource = ShaderFilename,
            ShaderTemplate = @"Shaders\transitions\_template.sksl"
        };
    }

    private string _ShaderFilename = @"Shaders\transitions\fade.sksl";
    public string ShaderFilename
    {
        get
        {
            return _ShaderFilename;
        }
        set
        {
            if (_ShaderFilename != value)
            {
                _ShaderFilename = value;

                if (Effect != null)
                    Effect.ShaderSource = value;

                OnPropertyChanged();
            }
        }
    }

    private ShaderTransitionEffect Effect { get; }

    private bool effectAttached;

    public override void Render(DrawingContext context)
    {
        if (Effect != null && !effectAttached)
        {
            effectAttached = true;

            VisualEffects.Add(Effect); //all the magic will be done with this effect
        }

        base.Render(context);
    }

    protected virtual void OnFromToChanged()
    {
        FromToChanged?.Invoke(this, null);
    }

    public event EventHandler FromToChanged;

    public virtual bool SetupFromTo()
    {
        IndexToLast = IndexTo;
        IndexFromLast = IndexFrom;

        var viewFrom = ChildrenFactory.GetViewForIndex(IndexFrom);
        var viewTo = ChildrenFactory.GetViewForIndex(IndexTo);

        if (viewFrom == null || viewTo == null)
        {
            throw new ApplicationException("Unexpected null");
        }

        if (Effect == null)
            return false;

        var modelSet = viewTo.BindingContext as SimpleItemViewModel;
        if (modelSet != null)
        {
            Effect.ControlFrom = viewFrom;
            Effect.ControlTo = viewTo;
            return true;
        }

        return false;

        //Debug.WriteLine($"Set new sources {IndexFrom} ({viewFrom.BindingContext}) <=> {IndexTo} ({viewTo.BindingContext}) at progress {progress:0.00}, scroll {ScrollProgress:0.00}");
    }

    private bool initialized;
    public override ScaledSize Measure(float widthConstraint, float heightConstraint, float scale)
    {
        initialized = false;

        Trace.WriteLine($"Carousel re-measured'");

        return base.Measure(widthConstraint, heightConstraint, scale);
    }


    protected override void OnChildrenInitialized()
    {

        IndexFrom = -1;
        IndexTo = -1;
        IndexFromLast = -1;
        IndexToLast = -1;
        initialized = false;

        base.OnChildrenInitialized();
    }

    protected override void OnScrollProgressChanged()
    {
        if (Effect == null)
            return;

        if (!initialized || ScrollProgress >= 0 && ScrollProgress <= 1) //ignore bouncing
        {
            var currentIndex = 0;
            if (ScrollProgress > 0)
                currentIndex = (int)Math.Floor((MaxIndex) * this.ScrollProgress);

            var progress = this.TransitionProgress;

            if (IndexFrom != currentIndex || !initialized)
            {
                if (currentIndex < MaxIndex)
                {
                    IndexTo = currentIndex + 1;
                    IndexFrom = currentIndex;

                    if (!initialized || IndexToLast != IndexTo || IndexFromLast != IndexFrom)
                    {
                        initialized = SetupFromTo();
                    }

                }
                else
                {
                    progress = 1.0;
                }

                OnFromToChanged();
            }

            Effect.Progress = progress;

            Effect.Update();
        }
    }



    //to skip default slides animation via translation, not calling base
    protected override void AnimateVisibleChild(SkiaControl view, Vector2 position)
    {
        if (Effect == null)
        {
            base.AnimateVisibleChild(view, position);
        }
    }

    private int IndexFrom = -1;
    private int IndexTo = -1;
    private int IndexFromLast = -1;
    private int IndexToLast = -1;

}