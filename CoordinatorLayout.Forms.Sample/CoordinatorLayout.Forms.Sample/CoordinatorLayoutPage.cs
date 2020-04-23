using System;
using Xamarin.Forms;

namespace CoordinatorLayout.Forms.Sample
{
    public class CoordinatorLayoutPage : ContentPage
    {
        private RelativeLayout _relativeLayout;
        private BoxView _topView;
        private BoxView _bottomView;

        public CoordinatorLayoutPage()
        {
            _relativeLayout = new RelativeLayout();

            _topView = new BoxView() {Color = Color.DodgerBlue};
            _relativeLayout.Children.Add(_topView,
                Constraint.Constant(0.0d),
                Constraint.Constant(0.0d),
                Constraint.RelativeToParent(parent => parent.Width),
                Constraint.FromExpression(() => 100.0d + _panTotal)
            );

            _bottomView = new BoxView() {Color = System.Drawing.Color.Orchid};
            _relativeLayout.Children.Add(_bottomView,
                Constraint.Constant(0.0d),
                Constraint.RelativeToView(_topView, (parent, otherView) => otherView.Height),
                Constraint.RelativeToParent(parent => parent.Width),
                Constraint.RelativeToView(_topView, (Parent, otherView) => Parent.Height - otherView.Height)
            );

            Content = _relativeLayout;

            var panGestureRecognizer = new PanGestureRecognizer();
            panGestureRecognizer.PanUpdated += PanGestureRecognizerOnPanUpdated;
            _relativeLayout.GestureRecognizers.Add(panGestureRecognizer);
        }

        private double _previousPanDistance = 0.0d;
        private double _panTotal = 0.0d;
        private double _initialPanTotal = 0.0d;

        private void PanGestureRecognizerOnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    _initialPanTotal = _panTotal;
                    break;
                case GestureStatus.Running:
                    _panTotal = _initialPanTotal + e.TotalY;
                    break;
                case GestureStatus.Completed:
                    _initialPanTotal = _panTotal;
                    break;
                case GestureStatus.Canceled:
                    _initialPanTotal = _panTotal;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _relativeLayout.ForceLayout();
        }
    }
}