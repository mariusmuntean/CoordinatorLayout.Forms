using System;

namespace CoordinatorLayout.XamarinForms
{
    public class ScrollEventArgs : EventArgs
    {
        public double Progress { get; }

        public ScrollEventArgs(double progress)
        {
            Progress = progress;
        }
    }
}