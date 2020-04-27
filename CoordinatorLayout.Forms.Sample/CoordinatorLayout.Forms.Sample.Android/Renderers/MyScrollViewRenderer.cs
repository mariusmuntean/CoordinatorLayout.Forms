using Android.Views;
using CoordinatorLayout.Forms.Sample.Android.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ScrollView), typeof(MyScrollViewRenderer))]

namespace CoordinatorLayout.Forms.Sample.Android.Renderers
{
    public class MyScrollViewRenderer : ScrollViewRenderer
    {
        public override bool DispatchTouchEvent(MotionEvent e)
        {

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