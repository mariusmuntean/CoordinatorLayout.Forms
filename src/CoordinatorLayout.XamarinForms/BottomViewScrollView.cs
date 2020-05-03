using System;
using Xamarin.Forms;

namespace CoordinatorLayout.XamarinForms
{
    public class ScrollUpdatedEventArgs : EventArgs
    {
        public double TotalX { get; }
        public double TotalY { get; }

        public ScrollUpdatedEventArgs(double totalX, double totalY)
        {
            TotalX = totalX;
            TotalY = totalY;
        }
    }

    internal class BottomViewScrollView : ScrollView
    {
        public void SendScrollUpdatedEvent(ScrollUpdatedEventArgs eventArgs)
        {
            ScrollUpdatedEvent?.Invoke(this, eventArgs);
        }

        public event EventHandler<ScrollUpdatedEventArgs> ScrollUpdatedEvent;
    }
}