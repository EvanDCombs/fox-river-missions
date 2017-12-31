using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CarouselView.FormsPlugin.Abstractions;

namespace FoxRiver
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DatabasePage : ContentPage
	{
        public ObservableCollection<Child> Children { get; set; }

		public DatabasePage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);
            Children = new ObservableCollection<Child>();
            RetrieveChildren();
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            ChildViewPager.ItemsSource = Children;
		}
        async void BackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async Task RetrieveChildren()
        {
            List<Child> children = await DatabaseAPI.GetChildrenAsync();
            if (Children.Count <= 0)
            {
                Children = new ObservableCollection<Child>(children);
                ChildViewPager.ItemsSource = Children;
            }
        }

    }
}