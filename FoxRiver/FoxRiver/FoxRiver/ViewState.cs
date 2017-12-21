using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FoxRiver
{
    public class ViewState
    {
        #region Height
        private readonly double minY = 0.1f;
        private readonly double maxY = 0.9f;
        private double Y
        {
            set
            {
                Console.WriteLine("Translation Amount: " + value);
                view.TranslationY = value + view.Y;
                Console.WriteLine("View Y: " + view.Y);
                /*if (view.Y <= minY)
                {
                    view.TranslateTo(0, minY);
                }
                else if (view.Y >= maxY)
                {
                    view.TranslateTo(0, maxY);
                }*/
            }
        }
        #endregion
        #region View
        private ContentView view;
        public ViewState(ContentView view, double screenHeight)
        {
            this.view = view;

            minY = screenHeight * 0.1;
            maxY = screenHeight * 0.9;
        }
        #endregion
        #region Methods
        public void Animate(double change)
        {
            Y = change;
        }
        public void AnimateToMin()
        {
            view.TranslateTo(0, maxY);
        }
        public void AnimateToMax()
        {
            view.TranslateTo(0, minY);
        }
        #endregion
    }
}
