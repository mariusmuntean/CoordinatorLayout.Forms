using System.ComponentModel;
using AppKit;
using CoordinatorLayout.XamarinForms.Renderers;
using CoordinatorLayout.XamarinForms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;


[assembly: ExportRenderer(typeof(BottomViewScrollView), typeof(BottomViewScrollViewRenderer))]

namespace CoordinatorLayout.XamarinForms.Renderers
{
    public class BottomViewScrollViewRenderer : ScrollViewRenderer
    {
        public BottomViewScrollViewRenderer()
        {
            this.ElementChanged += OnElementChanged;
        }

        private void OnElementChanged(object sender, VisualElementChangedEventArgs e)
        {
            if (e.OldElement != null)
            {
                e.OldElement.PropertyChanged -= HandlePropertyChanged;
            }

            if (e.NewElement != null)
            {
                e.NewElement.PropertyChanged += HandlePropertyChanged;
            }
        }

        void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == VisualElement.InputTransparentProperty.PropertyName)
            {
                // UpdateTouches();
            }

        }

        void UpdateTouches()
        {
            if (Element == null)
            {
                return;
            }

            this.AcceptsTouchEvents = !Element.InputTransparent;
        }

        public override void ScrollWheel(NSEvent theEvent)
        {
            base.ScrollWheel(theEvent);

            // If there is a XamarinForms BottomViewScrollView then pass the event
            if (this.Element is BottomViewScrollView scrollView)
            {
                // But only if we've reached the scroll bounds
                if (VerticalScroller.FloatValue <= 0.0f || VerticalScroller.FloatValue >= 1.0f)
                {
                    scrollView.SendScrollUpdatedEvent(new ScrollUpdatedEventArgs(theEvent.DeltaX, theEvent.DeltaY));
                }
            }
        }
    }
}