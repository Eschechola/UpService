using Xamarin.Forms;
using Android.Widget;
using Android.Content;
using System.ComponentModel;
using UpService.CustomViews;
using UpService.Droid.CustomRenderer;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RatingBarXF), typeof(RatingBarXFRenderer))]
namespace UpService.Droid.CustomRenderer
{
    public class RatingBarXFRenderer : ViewRenderer<RatingBarXF, RatingBar>
    {
        public RatingBarXFRenderer(Context context) : base(context) { }
        private RatingBar rbNative;
        protected override void OnElementChanged(ElementChangedEventArgs<RatingBarXF> e)
        {
            base.OnElementChanged(e);
            if(e.OldElement != null)
            {
                SetStepSize();
                SetNumberOfStarsSize();
                SetRating();
                rbNative.RatingBarChange -= EditRatingBarOnChanged;
            }
            if (e.NewElement != null)
            {
                rbNative = new RatingBar(Context);
                SetNativeControl(rbNative);
                SetStepSize();
                SetNumberOfStarsSize();
                SetRating();
                rbNative.RatingBarChange += EditRatingBarOnChanged;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == RatingBarXF.StepSizeProperty.PropertyName)
            {
                SetStepSize();
            }
            if (e.PropertyName == RatingBarXF.NumberOfStarsProperty.PropertyName)
            {
                SetNumberOfStarsSize();
            }
            if (e.PropertyName == RatingBarXF.RatingProperty.PropertyName)
            {
                SetRating();
            }
        }

        private void SetRating()
        {
            rbNative.Rating = Element.Rating;
        }

        private void SetNumberOfStarsSize()
        {
            rbNative.NumStars = Element.NumberOfStars;
        }

        private void SetStepSize()
        {
            rbNative.StepSize = Element.StepSize;
        }

        private void EditRatingBarOnChanged(object o, RatingBar.RatingBarChangeEventArgs e)
        {
            Element.Rating = Control.Rating;
        }
    }
}