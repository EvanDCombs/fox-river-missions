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
	public partial class DatabasePage : ContentPage
	{
		public DatabasePage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent ();
		}
        async void BackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}