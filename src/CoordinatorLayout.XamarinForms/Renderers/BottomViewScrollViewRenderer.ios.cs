using CoordinatorLayout.XamarinForms.Renderers;
using CoordinatorLayout.XamarinForms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BottomViewScrollView), typeof(BottomViewScrollViewRenderer))]

namespace CoordinatorLayout.XamarinForms.Renderers
{
    public class BottomViewScrollViewRenderer : ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            // ToDo: remove it at some point
            // Bounces = false;
        }
    }
}