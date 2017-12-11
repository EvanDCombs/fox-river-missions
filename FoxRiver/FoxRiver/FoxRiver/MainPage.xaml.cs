using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoxRiver
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
		}
        async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DatabasePage());
        }
    }
}
