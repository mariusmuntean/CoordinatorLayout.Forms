using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CoordinatorLayout.XamarinForms
{
    public partial class CoordinatorLayout : ContentView
    {
        private const string SnapToExtremesAnimation = "SnapToExtremesAnimation";
        private const string KineticScrollAnimationName = "KineticScrollAnimation";
        private const double PanThreshold = 1;

        private readonly RelativeLayout _relativeLayout;
        private View _topView;

        private View _bottomView;
        private BottomViewScrollView _bottomViewContainer;

        private ContentView _actionViewContainer;
        private View _actionView;

        private double _proportionalTopViewHeightMin = 0.1d;
        private double _proportionalTopViewHeightMax = 0.5d;
        private double _proportionalTopViewHeightInitial = 0.0d;
        private double _proportionalTopViewHeight = 0.0d;

        private double _panTotal = 0.0d;

        private PanGestureRecognizer _topViewPanGestureRecognizer;
        private PanGestureRecognizer _bottomViewPanGestureRecognizer;

        private double _bottomViewPreviousTotalY = 0.0d;
        private Direction _bottomViewPanDirection = Direction.none;
        private double _tempPanTotal = 0.0d;
        private double _bottomViewPanDelta = 0.0d;

        private bool _actionViewShowing = true;
        private Func<double, Direction> _directionCalculator;

        public CoordinatorLayout()
        {
            _directionCalculator = ChooseDirectionCalculator();

            _relativeLayout = new RelativeLayout();

#if !__MACOS__
            _bottomViewPanGestureRecognizer = new PanGestureRecognizer();
            _bottomViewPanGestureRecognizer.PanUpdated += BottomViewPanGestureRecognizerOnPanUpdated;
            // _bottomView.GestureRecognizers.Add(_bottomViewPanGestureRecognizer);
            _relativeLayout.GestureRecognizers.Add(_bottomViewPanGestureRecognizer);
#endif

            Content = _relativeLayout;
        }

        private Func<double, Direction> ChooseDirectionCalculator()
        {
            return Device.RuntimePlatform switch
            {
                Device.Android => (delta) => delta > 0.0d ? Direction.down : Direction.up,
                Device.iOS => (delta) => delta > 0.0d ? Direction.down : Direction.up,
                Device.macOS => (delta) => delta < 0.0d ? Direction.down : Direction.up,
                _ => (delta) => delta > 0.0d ? Direction.down : Direction.up
            };
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return;
            }

            if (propertyName == ProportionalTopViewHeightMinProperty.PropertyName || propertyName == ProportionalTopViewHeightMaxProperty.PropertyName)
            {
                if (ProportionalTopViewHeightMin <= ProportionalTopViewHeightMax)
                {
                    _proportionalTopViewHeightMin = ProportionalTopViewHeightMin;
                    _proportionalTopViewHeightMax = ProportionalTopViewHeightMax;

                    _proportionalTopViewHeightInitial = _proportionalTopViewHeightMin;
                    _proportionalTopViewHeight = _proportionalTopViewHeightInitial;

                    _relativeLayout.ForceLayout();
                }
            }

            if (propertyName == AutohideActionViewProperty.PropertyName)
            {
                if (!AutohideActionView)
                {
                    ShowActionView();
                }
            }

            if (propertyName == InitialExpansionStateProperty.PropertyName)
            {
                // Computing the exact value is not necessary as the Constraints for the top view will "clamp" it.
                _panTotal = InitialExpansionState switch
                {
                    InitialExpansionState.Collapsed => 0.0d,
                    InitialExpansionState.Expanded => double.MaxValue,
                    _ => 0.0d
                };
                _relativeLayout.ForceLayout();
            }

            if (propertyName == TopViewProperty.PropertyName)
            {
                // Replace all views
                ReplaceTopView();
                ReplaceBottomView();
                ReplaceActionView();
            }

            if (propertyName == BottomViewProperty.PropertyName)
            {
                // Replace only the bottom view (and its container)
                ReplaceBottomView();
            }

            if (propertyName == ActionViewProperty.PropertyName)
            {
                // Replace only the action view (and its container)
                ReplaceActionView();
            }
        }

        private void ReplaceActionView()
        {
            // remove any previous action view and container, if any
            if (_actionView != null)
            {
                _actionViewContainer.Content = null;
                _relativeLayout.Children.Remove(_actionViewContainer);
            }

            // add the new action view, if any and if a top view is available.
            if (ActionView != null && TopView != null)
            {
                // The action view is added to a container and that container is added to the relative layout
                _actionViewContainer = new ContentView
                {
                    Padding = new Thickness(0),
                    Margin = new Thickness(0),
                    IsClippedToBounds = false,
                    Content = ActionView
                };

                _relativeLayout.Children.Add(_actionViewContainer,
                    Constraint.Constant(0.0d),
                    Constraint.RelativeToView(_topView, (parent, view) => view.Height - (0.5 * ProportionalActionViewContainerHeight * parent.Height)),
                    Constraint.RelativeToParent(parent => parent.Width),
                    Constraint.RelativeToParent(parent => ProportionalActionViewContainerHeight * parent.Height)
                );

                // remember the new action view
                _actionView = ActionView;

                // handle the new action view 
                ShowHideActionView();
            }
        }

        private void ReplaceBottomView()
        {
            // replace any previous bottom view, if present
            if (_bottomView != null)
            {
                _bottomViewContainer.Scrolled -= BottomViewContainerOnScrolled;
                _bottomViewContainer.Content = null;
                _relativeLayout.Children.Remove(_bottomViewContainer);
            }

            // add the new bottom view, if any and if a top view is available.
            if (BottomView != null && TopView != null)
            {
                // the bottom view is added to a container and that container is added to the relative layout
                _bottomViewContainer = new BottomViewScrollView
                {
                    Content = BottomView,
                    InputTransparent = true,
                    CascadeInputTransparent = true,
                    Margin = new Thickness(5)
                };
                // _bottomViewContainer.Scrolled += BottomViewContainerOnScrolled;

                _relativeLayout.Children.Add(_bottomViewContainer,
                    Constraint.Constant(0.0d),
                    Constraint.RelativeToView(_topView, (parent, otherView) => otherView.Height + otherView.Margin.VerticalThickness),
                    Constraint.RelativeToParent(parent => parent.Width),
                    Constraint.RelativeToView(_topView, (Parent, otherView) => Parent.Height - otherView.Height)
                );

                // remember the new bottom view
                _bottomView = BottomView;
            }
        }

        private void BottomViewContainerOnScrolled(object sender, ScrolledEventArgs e)
        {
            var range = _bottomViewContainer.Content.Height - _bottomViewContainer.Height;
            var progress = e.ScrollY / range;
            ScrollEventHandler?.Invoke(this, new ScrollEventArgs(progress));
            ScrollProgress = progress;

            Console.WriteLine($"ScrollView.Y {e.ScrollY}");
        }

        private void ReplaceTopView()
        {
            // replace any previous top view, if present
            if (_topView != null)
            {
                _relativeLayout.Children.Remove(_topView);
            }

            // add the new top view, if any
            if (TopView != null)
            {
                _relativeLayout.Children.Add(TopView,
                    Constraint.Constant(0.0d),
                    Constraint.Constant(0.0d),
                    Constraint.RelativeToParent(parent => parent.Width),
                    TopViewHeightConstraint()
                );

                // remember the new top view
                _topView = TopView;

                // run the height-changed handler
                OnTopViewHeightChanged();
            }
        }

        private Constraint TopViewHeightConstraint()
        {
            return Constraint.RelativeToParent(parent =>
            {
                var topViewHeight = Math.Min(parent.Height * _proportionalTopViewHeightMax, Math.Max(parent.Height * _proportionalTopViewHeightMin, _panTotal));
                _proportionalTopViewHeight = topViewHeight / parent.Height;
                _panTotal = topViewHeight; // ToDo: investigate if this causes issues
                OnTopViewHeightChanged();
                return topViewHeight;
            });
        }

        private void OnTopViewHeightChanged()
        {
            var range = _proportionalTopViewHeightMax - _proportionalTopViewHeightMin;
            var progress = (_proportionalTopViewHeight - _proportionalTopViewHeightMin) / range;

            ExpansionEventHandler?.Invoke(this, new ExpansionEventArgs(progress));
            ExpansionProgress = progress;

            ShowHideActionView();
        }

        private void ShowHideActionView()
        {
            // Nothing to do as long as no ActionView/Container present
            if (_actionViewContainer?.Content == null)
            {
                return;
            }

            // If autohide isn't desired, then don't do anything
            if (!AutohideActionView)
            {
                return;
            }

            if (_proportionalTopViewHeight <= _proportionalTopViewHeightMin && _actionViewShowing)
            {
                HideActionView();
            }

            if (_proportionalTopViewHeight > _proportionalTopViewHeightMin && !_actionViewShowing)
            {
                ShowActionView();
            }
        }

        private void ShowActionView()
        {
            _actionViewShowing = true;
            _actionViewContainer.Content.FadeTo(1.0d, easing: Easing.CubicInOut);
            _actionViewContainer.Content.ScaleTo(1.0d, easing: Easing.CubicInOut);
        }

        private void HideActionView()
        {
            _actionViewShowing = false;
            _actionViewContainer.Content.FadeTo(0.0d, easing: Easing.CubicInOut);
            _actionViewContainer.Content.ScaleTo(0.0d, easing: Easing.CubicInOut);
        }

        private async void BottomViewPanGestureRecognizerOnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    _bottomViewPreviousTotalY = e.TotalY;

                    this.AbortAnimation(SnapToExtremesAnimation);
                    break;
                case GestureStatus.Running:

                    Console.WriteLine($"e.TotalY: {e.TotalY}");

                    _bottomViewPanDelta = e.TotalY - _bottomViewPreviousTotalY;
                    _bottomViewPanDirection = _directionCalculator(_bottomViewPanDelta);
                    _bottomViewPreviousTotalY = e.TotalY;

                    break;
                case GestureStatus.Completed:
                    OnPanGestureCompleted(_bottomViewPanDelta);
                    break;
                case GestureStatus.Canceled:
                    OnPanGestureCanceled(_bottomViewPanDelta);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (e.StatusType == GestureStatus.Running && _topView != null && _bottomView != null)
            {
                await HandlePan(PanSource.PanGesture, _bottomViewPanDelta);
            }
        }

        private async Task<bool> HandlePan(PanSource panSource, double panDelta)
        {
            // Stop the kinetic scrolling while the finger is on the screen
            if (panSource != PanSource.KineticScroll)
            {
                _bottomViewContainer.AbortAnimation(KineticScrollAnimationName);
            }

            if (_bottomViewContainer.ScrollY >= 0.0d && _bottomViewPanDirection == Direction.up && _proportionalTopViewHeight <= _proportionalTopViewHeightMin)
            {
                Console.WriteLine(".");
                Console.WriteLine($"_bottomViewContainer.ScrollY: {_bottomViewContainer.ScrollY}");

                var bottomViewScrollY = _bottomViewContainer.ScrollY + (panDelta);

                bottomViewScrollY = Clamp(bottomViewScrollY, 0.0d, _bottomViewContainer.Content.Height - _bottomViewContainer.Height);
                DebugWriteLine($"Scrolling up to: {bottomViewScrollY}");
                await _bottomViewContainer.ScrollToAsync(0.0d, bottomViewScrollY, false);
                return true;
            }
            else if (_bottomViewContainer.ScrollY > 0.0d && _bottomViewPanDirection == Direction.down && _proportionalTopViewHeight <= _proportionalTopViewHeightMin)
            {
                var bottomViewScrollY = _bottomViewContainer.ScrollY + (-panDelta);

                bottomViewScrollY = Clamp(bottomViewScrollY, 0.0d, _bottomViewContainer.Content.Height - _bottomViewContainer.Height);
                DebugWriteLine($"Scrolling down to: {bottomViewScrollY}");
                await _bottomViewContainer.ScrollToAsync(0.0d, bottomViewScrollY, false);
                return true;
            }
            else
            {
                // Don't increase if already at maximum && don't decrease if already at minimum
// #if __MACOS__
                if ((_bottomViewPanDirection == Direction.down && _proportionalTopViewHeight >= _proportionalTopViewHeightMax)
                    || (_bottomViewPanDirection == Direction.up && _proportionalTopViewHeight <= _proportionalTopViewHeightMin))
// #else
                    // if (panDelta > 0.0d && _proportionalTopViewHeight >= _proportionalTopViewHeightMax
                    //     || panDelta < 0.0d && _proportionalTopViewHeight <= _proportionalTopViewHeightMin)
// #endif
                {
                    return false;
                }

                // Don't expand/collapse during kinetic scrolling if not desired
                if (panSource == PanSource.KineticScroll && !ShouldExpandFromKineticScroll)
                {
                    return false;
                }

                DebugWriteLine($"Panning {_bottomViewPanDirection} to {_panTotal}");
                _panTotal += panDelta;
                _relativeLayout.ForceLayout();

                return true;
            }
        }

        private void OnPanGestureCompleted(double bottomViewPanDelta)
        {
            var isSnapping = Snap();
            if (!isSnapping)
            {
                LaunchKineticScroll(bottomViewPanDelta);
            }
        }

        private void OnPanGestureCanceled(double bottomViewPanDelta)
        {
            var isSnapping = Snap();
            if (!isSnapping)
            {
                LaunchKineticScroll(bottomViewPanDelta);
            }
        }

        private void LaunchKineticScroll(double bottomViewPanDelta)
        {
            // Don't kinetic scroll if it isn't desired
            if (!ShouldKineticScroll)
            {
                return;
            }

            // Don't kinetic scroll if the finger barely moved
            if (Math.Abs(bottomViewPanDelta) < PanThreshold)
            {
                return;
            }

            _bottomView.AbortAnimation(KineticScrollAnimationName);
            _bottomViewContainer.AnimateKinetic(KineticScrollAnimationName, (d, d1) =>
            {
                DebugWriteLine($"Kinetic: d={d}   d1={d1}");
                // _bottomViewContainer.ScrollToAsync(0, _bottomViewContainer.ScrollY + (Math.Sign(-d) * d1), false);
                var handled = HandlePan(PanSource.KineticScroll, Math.Sign(d) * d1).GetAwaiter().GetResult();

                // Returning true means "keep going"
                return handled;
            }, bottomViewPanDelta, KineticScrollDragCoefficient, OnKineticScrollingCompleted);
        }

        private void OnKineticScrollingCompleted()
        {
            OnPanGestureCompleted(0.0d);
        }

        private bool Snap()
        {
            // don't snap if snapping isn't desired
            if (!ShouldSnap)
            {
                return false;
            }

            // snap only if top view and bottom view are set
            if (_topView == null || _bottomView == null)
            {
                return false;
            }

            // No snap needed if the top view is either completely retracted or fully expanded
            if (Math.Abs(_proportionalTopViewHeight - _proportionalTopViewHeightMin) < double.Epsilon
                || Math.Abs(_proportionalTopViewHeight - _proportionalTopViewHeightMax) < double.Epsilon)
            {
                return false;
            }

            // Determine which extreme is closer
            var desiredProportionalTopViewHeight = _proportionalTopViewHeight;
            if (_proportionalTopViewHeight > _proportionalTopViewHeightMin + ProportionalSnapHeight * (_proportionalTopViewHeightMax - _proportionalTopViewHeightMin))
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

            return true;
        }

        private double Clamp(double value, double min, double max)
        {
            return Math.Min(max, Math.Max(min, value));
        }

        private static void DebugWriteLine(string message)
        {
#if DEBUG
            Console.WriteLine(message);
#endif
        }
    }
}