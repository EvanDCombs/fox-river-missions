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
        private string KenyaContent = "<html><head><style>body {font-family: 'Roboto', sans-serif;}p{color: #FFFFFF;font-size: 28pt;font-weight: bold;text-align: center;}a{color: #56C4C2;font-size: 28pt;font-weight: bold;text-align: center;}</style></head><body><p>Through <a href=''>Fox River</a> and<a href=''>MANNA</a></br><a href=''>Worldwide</a>, God is providing</br>primary education and nutrition</br>centers in the villages of</br><a href =''>Bomani</a> and <a href=''>Vipingo</a></p></body></html>";
        #region Initialization
        public MainPage()
		{
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
		}
        #endregion
        #region Interactions
        async void SponsorChildrenClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DatabasePage());
        }
        async void ChangeALifeClicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("http://foxrivermissions.org/join"));
        }
        #endregion
    }
}
