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
        private double viewHeight;
        private double percentageDragged;
        private double totalYDragged;
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
            viewHeight = height * 0.875;

            KenyaView.HeightRequest = viewHeight;
            NicaraguaView.HeightRequest = viewHeight;

            /*openKenyaTitleY = KenyaTitle.Y + KenyaView.Y;
            closedNicaraguaTitleY = NicaraguaTitle.Y + viewHeight;

            closedKenyaTitleY = viewHeight * 0.85;
            openNicaraguaTitleY = viewHeight * 0.05;*/
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
                OpenKenya(0);
            }
            else if (sender == NicaraguaTitle && openLocation == Locations.Kenya)
            {
                OpenNicaragua(0);
            }
        }
        async void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    percentageDragged = e.TotalY / viewHeight;
                    totalYDragged = e.TotalY;
                    if (sender == NicaraguaTitle && openLocation == Locations.Kenya && e.TotalY >= -(viewHeight * 0.825) && e.TotalY < 0)
                    {
                        KenyaView.TranslationY = e.TotalY;
                        KenyaTitle.TranslationY = -e.TotalY;
                        KenyaBackground.Opacity = KenyaContent.Opacity = UpArrow.Opacity = 1 + percentageDragged;

                        NicaraguaView.TranslationY = e.TotalY;
                        NicaraguaBackground.Opacity = NicaraguaContent.Opacity = DownArrow.Opacity = 0 - percentageDragged;
                    }
                    else if (sender == KenyaTitle && openLocation == Locations.Nicaragua && e.TotalY <= (viewHeight * 0.825) && e.TotalY > 0)
                    {
                        KenyaView.TranslationY = (e.TotalY - (viewHeight * 0.825));
                        KenyaTitle.TranslationY = -(e.TotalY - (viewHeight * 0.825));
                        KenyaBackground.Opacity = KenyaContent.Opacity = UpArrow.Opacity = 0 + percentageDragged;

                        NicaraguaView.TranslationY = (e.TotalY - (viewHeight * 0.825));
                        NicaraguaBackground.Opacity = NicaraguaContent.Opacity = DownArrow.Opacity = 1 - percentageDragged;
                    }
                    break;
                case GestureStatus.Completed:
                    Console.WriteLine(totalYDragged);
                    if (sender == NicaraguaTitle && openLocation == Locations.Kenya)
                    {
                        if (totalYDragged <= -75)
                        {
                            OpenNicaragua(percentageDragged);
                        }
                        else
                        {
                            OpenKenya(percentageDragged);
                        }
                    }
                    else if (sender == KenyaTitle && openLocation == Locations.Nicaragua)
                    {
                        if (totalYDragged >= 75)
                        {
                            OpenKenya(percentageDragged);
                        }
                        else
                        {
                            OpenNicaragua(percentageDragged);
                        }
                    }
                    break;
            }
        }
        #endregion
        #region Animations
        private void OpenKenya(double percentage)
        {
            uint length = (uint)(500 * (1 - Math.Abs(percentage)));

            //Animates Kenya Section
            KenyaView.TranslateTo(0, 0, length);
            KenyaTitle.TranslateTo(0, 0, length);
            KenyaBackground.FadeTo(1, length);
            KenyaContent.FadeTo(1, length);
            DownArrow.FadeTo(0, length);

            //Animates Nicaragua Section
            NicaraguaView.TranslateTo(0, 0, length);
            NicaraguaBackground.FadeTo(0, length);
            NicaraguaContent.FadeTo(0, length);
            UpArrow.FadeTo(1, length);
            
            openLocation = Locations.Kenya;
        }
        private void OpenNicaragua(double percentage)
        {
            uint length = (uint)(500 * (1 - Math.Abs(percentage)));

            //Animates Kenya Section
            KenyaView.TranslateTo(0, -(viewHeight * 0.825), length);
            KenyaTitle.TranslateTo(0, (viewHeight * 0.825), length);
            KenyaBackground.FadeTo(0, length);
            KenyaContent.FadeTo(0, length);
            DownArrow.FadeTo(1, length);

            //Animates Nicaragua Section
            NicaraguaView.TranslateTo(0, -(viewHeight * 0.825), length);
            NicaraguaBackground.FadeTo(1, length);
            NicaraguaContent.FadeTo(1, length);
            UpArrow.FadeTo(0, length);
            
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
