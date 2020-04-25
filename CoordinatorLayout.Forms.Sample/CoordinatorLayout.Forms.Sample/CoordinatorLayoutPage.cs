using System;
using Xamarin.Forms;

namespace CoordinatorLayout.Forms.Sample
{
    public class CoordinatorLayoutPage : ContentPage
    {
        private const string SnapToExtremesAnimation = "SnapToExtremesAnimation";
        private RelativeLayout _relativeLayout;
        private View _topView;
        private ScrollView _bottomView;

        private double _proportionalTopViewHeightMin = 0.1d;
        private double _proportionalTopViewHeightMax = 0.5d;
        private double _proportionalTopViewHeightInitial = 0.0d;
        private double _proportionalTopViewHeight = 0.0d;

        public CoordinatorLayoutPage()
        {
            _proportionalTopViewHeightInitial = _proportionalTopViewHeightMin;
            _proportionalTopViewHeight = _proportionalTopViewHeightInitial;

            _relativeLayout = new RelativeLayout();

            _topView = new BoxView() {Color = Color.DodgerBlue};
            _relativeLayout.Children.Add(_topView,
                Constraint.Constant(0.0d),
                Constraint.Constant(0.0d),
                Constraint.RelativeToParent(parent => parent.Width),
                TopViewHeightConstraint()
            );

            _bottomView = GetBottomView();
            _relativeLayout.Children.Add(_bottomView,
                Constraint.Constant(0.0d),
                Constraint.RelativeToView(_topView, (parent, otherView) => otherView.Height),
                Constraint.RelativeToParent(parent => parent.Width),
                Constraint.RelativeToView(_topView, (Parent, otherView) => Parent.Height - otherView.Height)
            );

            Content = _relativeLayout;

            // _topViewPanGestureRecognizer = new PanGestureRecognizer();
            // _topViewPanGestureRecognizer.PanUpdated += TopViewPanGestureRecognizerOnPanUpdated;
            // _topView.GestureRecognizers.Add(_topViewPanGestureRecognizer);

            _bottomViewPanGestureRecognizer = new PanGestureRecognizer();
            _bottomViewPanGestureRecognizer.PanUpdated += BottomViewPanGestureRecognizerOnPanUpdated;
            // _bottomView.GestureRecognizers.Add(_bottomViewPanGestureRecognizer);
            _relativeLayout.GestureRecognizers.Add(_bottomViewPanGestureRecognizer);
        }

        private Constraint TopViewHeightConstraint()
        {
            return Constraint.RelativeToParent(parent =>
            {
                var topViewHeight = Math.Min(parent.Height * _proportionalTopViewHeightMax, Math.Max(parent.Height * _proportionalTopViewHeightMin, _panTotal));
                _proportionalTopViewHeight = topViewHeight / parent.Height;
                OnTopviewHeightChanged();
                return topViewHeight;
            });
        }

        private void OnTopviewHeightChanged()
        {
        }

        private double _previousPanDistance = 0.0d;

        private double _panTotal = 0.0d;

        private PanGestureRecognizer _topViewPanGestureRecognizer;
        private PanGestureRecognizer _bottomViewPanGestureRecognizer;

        enum direction
        {
            up,
            none,
            down
        }

        private double _bottomViewPreviousTotalY = 0.0d;
        private direction _bottomViewPanDirection = direction.none;
        private double _tempPanTotal = 0.0d;
        private double _bottomViewPanDelta = 0.0d;

        private async void BottomViewPanGestureRecognizerOnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    _bottomViewPreviousTotalY = e.TotalY;

                    this.AbortAnimation(SnapToExtremesAnimation);
                    break;
                case GestureStatus.Running:

                    _bottomViewPanDelta = e.TotalY - _bottomViewPreviousTotalY;
                    _bottomViewPanDirection = _bottomViewPanDelta > 0.0d ? direction.down : direction.up;
                    _bottomViewPreviousTotalY = e.TotalY;

                    break;
                case GestureStatus.Completed:
                    OnPanGestureCompleted();
                    break;
                case GestureStatus.Canceled:
                    OnPanGestureCanceled();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Console.WriteLine($"Raw tempPanTotal: {_tempPanTotal}");
            Console.WriteLine($"Direction: {_bottomViewPanDirection}");

            if (e.StatusType == GestureStatus.Running)
            {
                if (_bottomView.ScrollY >= 0.0d && _bottomViewPanDirection == direction.up && _proportionalTopViewHeight <= _proportionalTopViewHeightMin)
                {
                    var bottomViewScrollY = _bottomView.ScrollY + (-_bottomViewPanDelta);

                    bottomViewScrollY = Clamp(bottomViewScrollY, 0.0d, _bottomView.Content.Height - _bottomView.Height);
                    Console.WriteLine($"Scrolling up to: {bottomViewScrollY}");
                    await _bottomView.ScrollToAsync(0.0d, bottomViewScrollY, false);
                }
                else if (_bottomView.ScrollY > 0.0d && _bottomViewPanDirection == direction.down && _proportionalTopViewHeight <= _proportionalTopViewHeightMin)
                {
                    var bottomViewScrollY = _bottomView.ScrollY + (-_bottomViewPanDelta);

                    bottomViewScrollY = Clamp(bottomViewScrollY, 0.0d, _bottomView.Content.Height - _bottomView.Height);
                    Console.WriteLine($"Scrolling down to: {bottomViewScrollY}");
                    await _bottomView.ScrollToAsync(0.0d, bottomViewScrollY, false);
                }
                else
                {
                    Console.WriteLine($"Panning {_bottomViewPanDirection} to {_panTotal}");
                    _panTotal += _bottomViewPanDelta;
                    _relativeLayout.ForceLayout();
                }
            }

            // _relativeLayout.ForceLayout();
        }

        private void OnPanGestureCompleted()
        {
            Snap();
        }

        private void OnPanGestureCanceled()
        {
            Snap();
        }

        private void Snap()
        {
            // No snap needed if the top view is either completely retracted or fully expanded
            if (_proportionalTopViewHeight == _proportionalTopViewHeightMin || _proportionalTopViewHeight == _proportionalTopViewHeightMax)
            {
                return;
            }

            // Determine which extreme is closer
            var desiredProportionalTopViewHeight = _proportionalTopViewHeight;
            if (_proportionalTopViewHeight > 0.5d * (_proportionalTopViewHeightMin + (_proportionalTopViewHeightMax - _proportionalTopViewHeightMin)))
            {
                desiredProportionalTopViewHeight = _proportionalTopViewHeightMax;
            }
            else
            {
                desiredProportionalTopViewHeight = _proportionalTopViewHeightMin;
            }

            var snapAnimation = new Animation(d =>
            {
                _panTotal = d * _relativeLayout.Height;
                _relativeLayout.ForceLayout();
            }, _proportionalTopViewHeight, desiredProportionalTopViewHeight, Easing.CubicInOut);
            this.AbortAnimation(SnapToExtremesAnimation);
            snapAnimation.Commit(this, SnapToExtremesAnimation);
        }

        private double Clamp(double value, double min, double max)
        {
            return Math.Min(max, Math.Max(min, value));
        }

        private ScrollView GetBottomView()
        {
            var rand = new Random();
            var stackLayout = new StackLayout()
            {
                InputTransparent = true,
                CascadeInputTransparent = true
            };
            var i = 0;
            for (; i < 15; i++)
            {
                stackLayout.Children.Add(new BoxView()
                {
                    Color = new Color(rand.NextDouble(), rand.NextDouble(), rand.NextDouble()).WithLuminosity(0.8).WithSaturation(0.8),
                    InputTransparent = true
                });
            }

            var scrollView = new ScrollView
            {
                Content = stackLayout,
                InputTransparent = true,
                CascadeInputTransparent = true
            };
            // scrollView.Scrolled += ScrollViewOnScrolled;

            return scrollView;
        }

        private double _previousScrollY = 0.0d;

        enum scrollDirection
        {
            up,
            none,
            down
        }

        private scrollDirection _scrollDirection = scrollDirection.none;
    }
}