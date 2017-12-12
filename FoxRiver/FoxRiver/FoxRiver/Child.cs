using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FoxRiver
{
    public class Child : INotifyPropertyChanged
    {
        #region Binded Properties
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }
        private string school;
        public string School
        {
            get { return school; }
            set
            {
                school = value;
                NotifyPropertyChanged("School");
            }
        }
        private string image;
        public string Image
        {
            get { return image; }
            set
            {
                image = "http://students.hope.indibits.com/uploads/images/" + value + "_r.jpg";
                NotifyPropertyChanged("Image");
            }
        }
        public string Content
        {
            get
            {
                StringBuilder builder = new StringBuilder("Grade: " + Grade);
                builder.AppendLine("Birthday: " + Birthday);
                builder.AppendLine("Sex: " + Sex);
                builder.AppendLine("Hobbies: " + Hobbies);
                builder.AppendLine("Fav. Subjects: " + Subject);
                builder.AppendLine("Siblings: " + Siblings);
                builder.AppendLine("Family Income: " + FamilyIncome);
                return builder.ToString();
            }
            set { NotifyPropertyChanged("Content"); }
        }
        #endregion
        #region Properties
        public int Grade { get; set; }
        private DateTime birthday;
        public DateTime Birthday { get; set; }
        public string Sex { get; set; }
        public float FamilyIncome { get; set; }
        public int Siblings { get; set; }
        public string Subject { get; set; }
        public string Hobbies { get; set; }
        #endregion
        #region Initialization
        public Child() { }
        public Child(string name, string school)
        {
            this.Name = name;
            this.School = school;
        }
        public Child(JToken token)
        {
            token = token.First;

            this.Name = (string)token.SelectToken("name");
            this.School = token.SelectToken("school_id").ToString();
            this.Grade = (int)token.SelectToken("class_id"); ;
            DateTime.TryParse((string)token.SelectToken("date_of_birth"), out birthday);
            this.Sex = (string)token.SelectToken("gender");
            this.FamilyIncome = (float)token.SelectToken("family_home_info.family_monthly_income_usd");
            this.Siblings = (int)token.SelectToken("family_home_info.number_of_siblings");
            this.Subject = GetArray(token, "subjects");
            this.Hobbies = GetArray(token, "hobbies");
            this.Image = (string)token.SelectToken("profile_picture");
        }
        #endregion
        #region Methods
        private string GetArray(JToken token, string id)
        {
            JArray array = (JArray)token.SelectToken(id);
            StringBuilder stringBuilder = null;
            foreach(JToken jtoken in array)
            {
                if (stringBuilder == null)
                {
                    stringBuilder = new StringBuilder((string)jtoken.SelectToken("name"));
                }
                else
                {
                    stringBuilder.Append(", " + (string)jtoken.SelectToken("name"));
                }
            }
            return stringBuilder.ToString();
        }
        #endregion
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
