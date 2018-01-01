using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FoxRiver
{
	public class ViewPager : ContentView
	{
        #region Fields and Properties
        private int currentIndex;
        private ChildView currentView;
        private PanGestureRecognizer panGesture;
        private List<ChildView> views;
        private RelativeLayout layout;

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
        #endregion
        #region Initialization
        public ViewPager ()
		{
            views = new List<ChildView>();

            panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += (s, e) => {

            };

            layout = new RelativeLayout();
            Content = layout;
        }
        #endregion
        #region Methods
        private void LoadChildren()
        {
            currentIndex = 0;
            currentView = GetView(children[currentIndex]);
            AnimateInFromRight(currentView);
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

                views.Add(childView);
                layout.Children.Add(childView, Constraint.RelativeToParent((parent) => {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) => {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) => {
                    return parent.Width;
                }),
                Constraint.RelativeToParent((parent) => {
                    return parent.Height;
                }));
            }

            return childView;
        }

        private void AnimateInFromRight(ChildView view)
        {
            view.TranslateTo(-this.Width, 0, 1000);
        }
        #endregion
    }
}