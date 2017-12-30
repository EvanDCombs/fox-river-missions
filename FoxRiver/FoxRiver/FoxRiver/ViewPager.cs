using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FoxRiver
{
	public class ViewPager : ContentView
	{
        private int currentIndex;
        private ChildView currentView;
        private PanGestureRecognizer panGesture;
        private List<ChildView> views;
        private AbsoluteLayout layout;

        private List<Child> children = new List<Child>();
        public List<Child> SponsorChildren
        {
            get { return children; }
            set
            {
                children = value;
                LoadChildren();
            }
        }

		public ViewPager ()
		{
            views = new List<ChildView>();

            panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += (s, e) => {

            };

            layout = new AbsoluteLayout();
            Content = layout;
        }

        private void LoadChildren()
        {
            currentIndex = 0;
            currentView = GetView(children[currentIndex]);
        }

        private ChildView GetView(Child child)
        {
            ChildView childView = null;

            foreach(ChildView view in views)
            {
                if (view != currentView)
                {
                    childView = view;
                }
            }
            if (childView == null)
            {
                childView = new ChildView();
                childView.BindingContext = child;

                AbsoluteLayout.SetLayoutFlags(childView, AbsoluteLayoutFlags.All);
                AbsoluteLayout.SetLayoutBounds(childView, new Rectangle(0, 0, 1, 1));

                views.Add(childView);
                layout.Children.Add(childView);
            }

            return childView;
        }
	}
}