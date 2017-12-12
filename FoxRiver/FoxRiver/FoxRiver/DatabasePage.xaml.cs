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
        public List<Child> Children { get; set; }

		public DatabasePage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);
            Children = RetrieveChildren();
            InitializeComponent ();

            ContentView.ItemsSource = Children;
		}
        async void BackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        public List<Child> RetrieveChildren()
        {
            List<Child> list = new List<Child>();

            list.Add(new Child("Xavier", "Old School"));
            list.Add(new Child("Atilla", "New School"));
            list.Add(new Child("Ceasar", "Old School"));
            list.Add(new Child("Boudica", "Old School"));
            list.Add(new Child("Eleazar", "New School"));
            list.Add(new Child("Tyler", "Old School"));

            return list;
        }
    }
}