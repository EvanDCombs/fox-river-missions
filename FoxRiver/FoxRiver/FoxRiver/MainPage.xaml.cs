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
        private string KenyaHtml = "<html><head><style>body {font-family: 'Roboto', sans-serif;}p{color: #FFFFFF;font-size: 28pt;font-weight: bold;text-align: center;}a{color: #56C4C2;font-size: 28pt;font-weight: bold;text-align: center;}</style></head><body><p>Through <a href=''>Fox River</a> and<a href=''>MANNA</a></br><a href=''>Worldwide</a>, God is providing</br>primary education and nutrition</br>centers in the villages of</br><a href =''>Bomani</a> and <a href=''>Vipingo</a></p></body></html>";
        private Locations openLocation = Locations.Kenya;
        private double height = 0;
        private double openKenyaTitleY = 0;
        private double closedKenyaTitleY = 0;
        private double openNicaraguaTitleY = 0;
        private double closedNicaraguaTitleY = 0;
        #region Initialization
        public MainPage()
		{
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
		}
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            this.height = height;
            double viewHeight = height * 0.875;

            KenyaView.HeightRequest = viewHeight;
            NicaraguaView.HeightRequest = viewHeight;

            openKenyaTitleY = KenyaTitle.Y + KenyaView.Y;
            closedNicaraguaTitleY = NicaraguaTitle.Y + viewHeight;

            closedKenyaTitleY = viewHeight * 0.85;
            openNicaraguaTitleY = viewHeight * 0.05;
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
        async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (sender == KenyaTitle && openLocation == Locations.Nicaragua)
            {
                OpenKenya(closedKenyaTitleY, openNicaraguaTitleY);
            }
            else if (sender == NicaraguaTitle && openLocation == Locations.Kenya)
            {
                OpenNicaragua(openKenyaTitleY, closedNicaraguaTitleY);
            }
        }
        private void Scroll_Scrolled(object sender, ScrolledEventArgs e)
        {
            
        }
        #endregion
        #region Animations
        private void OpenKenya(double kenyaTitleY, double nicaraguaTitleY)
        {
            Scroll.ScrollToAsync(KenyaView, ScrollToPosition.Start, true);
            NicaraguaBackground.FadeTo(0);
            NicaraguaContent.FadeTo(0);
            KenyaBackground.FadeTo(1);
            KenyaContent.FadeTo(1);
            DownArrow.FadeTo(0);
            UpArrow.FadeTo(1);
            KenyaTitle.TranslateTo(0, 0, 250);
            NicaraguaTitle.TranslateTo(0, 0, 250);
            openLocation = Locations.Kenya;
        }
        private void OpenNicaragua(double kenyaTitleY, double nicaraguaTitleY)
        {
            Scroll.ScrollToAsync(NicaraguaView, ScrollToPosition.Start, true);
            NicaraguaBackground.FadeTo(1);
            NicaraguaContent.FadeTo(1);
            KenyaBackground.FadeTo(0);
            KenyaContent.FadeTo(0);
            DownArrow.FadeTo(1);
            UpArrow.FadeTo(0);
            KenyaTitle.TranslateTo(0, closedKenyaTitleY, 250);
            NicaraguaTitle.TranslateTo(0, openNicaraguaTitleY, 250);
            openLocation = Locations.Nicaragua;
        }
        #endregion
        #region Enums
        public enum Locations
        {
            Kenya,
            Nicaragua
        }
        #endregion
    }
}
