using Xamarin.Forms;

namespace CoordinatorLayout.XamarinForms
{
    public partial class CoordinatorLayout
    {
        public static readonly BindableProperty TopViewProperty = BindableProperty.Create(
            nameof(TopView),
            typeof(View),
            typeof(CoordinatorLayout)
        );

        public View TopView
        {
            get => (View) GetValue(TopViewProperty);
            set => SetValue(TopViewProperty, value);
        }
        
        
        public static readonly BindableProperty BottomViewProperty = BindableProperty.Create(
            nameof(BottomView),
            typeof(View),
            typeof(CoordinatorLayout)
        );

        public View BottomView
        {
            get => (View) GetValue(BottomViewProperty);
            set => SetValue(BottomViewProperty, value);
        }
        
        
        public static readonly BindableProperty ActionViewProperty = BindableProperty.Create(
            nameof(ActionView),
            typeof(View),
            typeof(CoordinatorLayout)
        );

        public View ActionView
        {
            get => (View) GetValue(ActionViewProperty);
            set => SetValue(ActionViewProperty, value);
        }
    }
}