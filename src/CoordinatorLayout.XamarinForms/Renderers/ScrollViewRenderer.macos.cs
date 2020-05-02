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
            // if (e.PropertyName == PlatformConfiguration.iOSSpecific.ScrollView.ShouldDelayContentTouchesProperty.PropertyName)
            //     UpdateDelaysContentTouches();
            // else
            // if (e.PropertyName == ScrollView.ContentSizeProperty.PropertyName)
            //     UpdateContentSize();
            // else if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName)
            //     UpdateBackgroundColor();
            // else
            if (e.PropertyName == VisualElement.InputTransparentProperty.PropertyName)
            {
                // UpdateTouches();
            }

            // else if (e.PropertyName == ScrollView.VerticalScrollBarVisibilityProperty.PropertyName)
            //     UpdateVerticalScrollBarVisibility();
            // else if (e.PropertyName == ScrollView.HorizontalScrollBarVisibilityProperty.PropertyName)
            //     UpdateHorizontalScrollBarVisibility();
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
            if (this.Element != null && this.Element.InputTransparent)
            {
                // don't pass through scroll events when InputTransparent is true
            }
            else
            {
                base.ScrollWheel(theEvent); 
            }
        }
    }
}