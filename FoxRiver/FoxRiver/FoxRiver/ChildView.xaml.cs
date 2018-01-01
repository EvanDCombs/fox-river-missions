using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoxRiver
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChildView : ContentView
	{
		public ChildView ()
		{
			InitializeComponent ();
		}

        private void CachedImage_Finish(object sender, FFImageLoading.Forms.CachedImageEvents.FinishEventArgs e)
        {
            PImage.FadeTo(1, 150);
            ProfileImage.FadeTo(1, 250);
        }
    }
}