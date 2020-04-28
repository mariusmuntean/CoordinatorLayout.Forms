using Android.Views;
using CoordinatorLayout.XamarinForms.Renderers;
using CoordinatorLayout.XamarinForms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BottomViewScrollView), typeof(BottomViewScrollViewRenderer))]

namespace CoordinatorLayout.XamarinForms.Renderers
{
    public class BottomViewScrollViewRenderer : ScrollViewRenderer
    {
        public override bool DispatchTouchEvent(MotionEvent e)
        {
            // For some reason the ScrollView on Android doesn't fully respect the "InputTransparent" so it has to be done here. No worries, multi-targeting is great!.
            if (Element.InputTransparent)
            {
                return false;
            }

            return base.DispatchTouchEvent(e);
        }

        public override bool OnTouchEvent(MotionEvent ev)
        {
            return base.OnTouchEvent(ev);
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            if (Element.InputTransparent)
            {
                return false;
            }

            return base.OnInterceptTouchEvent(ev);
        }
    }
}