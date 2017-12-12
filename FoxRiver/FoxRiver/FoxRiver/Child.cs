using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace FoxRiver
{
    public class Child : INotifyPropertyChanged
    {
        private string name;
        private string school;
        private string image;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }
        public string School
        {
            get { return school; }
            set
            {
                school = value;
                NotifyPropertyChanged("School");
            }
        }
        public string Image
        {
            get { return image; }
            set
            {
                image = value;
                NotifyPropertyChanged("Image");
            }
        }

        public Child(string name, string school)
        {
            this.Name = name;
            this.School = school;
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
