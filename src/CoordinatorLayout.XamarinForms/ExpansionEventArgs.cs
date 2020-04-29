using System;

namespace CoordinatorLayout.XamarinForms
{
    public class ExpansionEventArgs : EventArgs
    {
        public double Progress { get; }

        public ExpansionEventArgs(double progress)
        {
            Progress = progress;
        }
    }
}