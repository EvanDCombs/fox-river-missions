using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CarouselView.FormsPlugin.Abstractions;

namespace FoxRiver
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DatabasePage : ContentPage, INotifyPropertyChanged
    {
        private bool isLoading = true;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                NotifyPropertyChanged("IsLoading");
            }
        }
        public ObservableCollection<Child> Children { get; set; }

		public DatabasePage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);
            Children = new ObservableCollection<Child>();
            RetrieveChildren();
            InitializeComponent();
            IsLoading = true;

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
                LoadingIndicator.IsVisible = false;
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}