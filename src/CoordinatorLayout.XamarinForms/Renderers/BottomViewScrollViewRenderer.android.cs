using System;
using Android.Views;
using CoordinatorLayout.XamarinForms.Renderers;
using CoordinatorLayout.XamarinForms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content;

[assembly: ExportRenderer(typeof(BottomViewScrollView), typeof(BottomViewScrollViewRenderer))]

namespace CoordinatorLayout.XamarinForms.Renderers
{
    public class BottomViewScrollViewRenderer : ScrollViewRenderer
    {

        public BottomViewScrollViewRenderer(Context context) : base(context)
        {
            
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
        }

        public override bool DispatchTouchEvent(MotionEvent e)
        {
            // For some reason the ScrollView on Android doesn't fully respect the "InputTransparent" so it has to be done here. No worries, multi-targeting is great!.
            // if (Element.InputTransparent)
            // {
            //     return false;
            // }

            return base.DispatchTouchEvent(e);
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            // if (Element != null && Element.InputTransparent)
            // {
                // return false;
            // }
            //
            return base.OnInterceptTouchEvent(ev);
        }

        public override bool OnTouchEvent(MotionEvent ev)
        {
            // if (Element != null && Element.InputTransparent)
            // {
                return false;
            // }
            //
            // return base.OnTouchEvent(ev);
        }

    }
}