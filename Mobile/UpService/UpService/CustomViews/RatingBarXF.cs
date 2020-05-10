using Xamarin.Forms;

namespace UpService.CustomViews
{
    public class RatingBarXF : View
    {
        public static readonly BindableProperty NumberOfStarsProperty = BindableProperty.Create("NumberOfStars", typeof(int), typeof(RatingBarXF), 5);
        public static readonly BindableProperty StepSizeProperty = BindableProperty.Create("StepSize", typeof(float), typeof(RatingBarXF), 1f);
        public static readonly BindableProperty RatingProperty = BindableProperty.Create("Rating", typeof(float), typeof(RatingBarXF), 0f);

        public int NumberOfStars
        {
            get => (int)GetValue(NumberOfStarsProperty);
            set => SetValue(NumberOfStarsProperty, value);
        }
        public float StepSize
        {
            get => (float)GetValue(StepSizeProperty);
            set => SetValue(StepSizeProperty, value);
        }
        public float Rating
        {
            get => (float)GetValue(RatingProperty);
            set => SetValue(RatingProperty, value);
        }
    }
}
