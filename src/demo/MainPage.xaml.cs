using AppoMobi.Specials;
using DrawnUi.Maui.Draw;
using ShadersCarousel.Models;
using ShadersCarouselDemo.Controls.Carousel;
using System.Windows.Input;

namespace ShadersCarouselDemo
{

    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            try
            {
                InitializeComponent();

                BindingContext = this;

                _mock = new MockDataProvider();

                var data = _mock.GetRandomItems(10);

                Items.AddRange(data);

                MakeSlowerRatio = 2.1;
            }
            catch (Exception e)
            {
                Super.DisplayException(this, e);
            }
        }
        private readonly MockDataProvider _mock;

        public ObservableRangeCollection<SimpleItemViewModel> Items { get; } = new();

        private int _SelectedIndex;
        public int SelectedIndex
        {
            get
            {
                return _SelectedIndex;
            }
            set
            {
                if (_SelectedIndex != value)
                {
                    _SelectedIndex = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _slower;
        public double MakeSlowerRatio
        {
            get
            {
                return _slower;
            }
            set
            {
                if (_slower != value)
                {
                    _slower = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DisplaySpeed));
                    MainCarousel.SpeedRatio = MakeSlowerRatio;
                }
            }
        }

        public double DisplaySpeed => SliderSpeed.Max - MakeSlowerRatio + SliderSpeed.Min;

        private bool _AutoPlay;
        public bool AutoPlay
        {
            get
            {
                return _AutoPlay;
            }
            set
            {
                if (_AutoPlay != value)
                {
                    _AutoPlay = value;
                    OnPropertyChanged();
                    MakeSlowerRatio = value ? 3.9 : 2.1;
                    if (value && !MainCarousel.InTransition)
                    {
                        MainCarousel.GoOne();
                    }
                }
            }
        }

        private bool _AutoLoop;
        public bool AutoLoop
        {
            get
            {
                return _AutoLoop;
            }
            set
            {
                if (_AutoLoop != value)
                {
                    _AutoLoop = value;
                    OnPropertyChanged();
                    if (value)
                    {
                        MainCarousel.PlayingType = PlayType.Next;
                    }
                    else
                    {
                        MainCarousel.PlayingType = PlayType.Default;
                    }
                }
            }
        }

        public ICommand CommandGoNext
        {
            get
            {
                return new Command((object context) =>
                {
                    MainCarousel.GoNext();
                });
            }
        }

        public ICommand CommandGoPrev
        {
            get
            {
                return new Command((object context) =>
                {
                    MainCarousel.GoPrev();
                });
            }
        }



        private async void OnTransitionChanged(object sender, bool b)
        {
            if (AutoPlay && !b && !MainCarousel.IsUserFocused)
            {
                //kick next
                await Task.Delay(1000);
                MainCarousel.GoOne();
            }
        }

        private void SkiaControl_OnLayoutIsReady(object sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                MainCanvas.FadeTo(1, 750);
            });
        }
    }

}

