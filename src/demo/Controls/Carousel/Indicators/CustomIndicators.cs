namespace ShadersCarouselDemo.Controls.Carousel.Indicators;

public class CustomIndicators : CarouselIndicators
{
    public override void UpdateSource()
    {
        var values = new List<SelectedLabel>();
        for (int i = 0; i < TotalEnabled; i++)
        {
            var text = $"{i + 1}";
            values.Add(new()
            {
                Text = text,
                IsSelected = i == SelectedIndex
            });
        }

        Values = values;
        MainThread.BeginInvokeOnMainThread(() =>
        {
            ItemsSource = Values;
        });
    }
}