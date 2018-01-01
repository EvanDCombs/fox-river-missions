using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Xamarin.Forms;

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
                image = value;
                NotifyPropertyChanged("Image");
            }
        }
        public string Content
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("Grade: " + Grade);
                builder.AppendLine("Birthday: " + birthday.ToShortDateString());
                builder.AppendLine("Sex: " + Sex);
                builder.AppendLine("Hobbies: " + Hobbies);
                builder.AppendLine("Fav. Subjects: " + Subject);
                builder.AppendLine("Siblings: " + Siblings);
                builder.AppendLine("Family Income: $" + FamilyIncome);
                return builder.ToString();
            }
            set { NotifyPropertyChanged("Content"); }
        }
        #endregion
        #region Properties
        public string Grade { get; set; }
        private DateTime birthday;
        public string Sex { get; set; }
        public float FamilyIncome { get; set; }
        public int Siblings { get; set; }
        public string Subject { get; set; }
        public string Hobbies { get; set; }
        #endregion
        #region Initialization
        public Child() { }
        private Child(string name, string school)
        {
            this.Name = name;
            this.School = school;
        }
        private Child(JToken token, Organization organization)
        {
            token = token.First;

            this.Name = (string)token.SelectToken("name");
            this.School = SchoolSwitch((int)token.SelectToken("school_id"), organization);
            this.Grade = GradeSwitch((int)token.SelectToken("class_id"), organization);
            DateTime.TryParse((string)token.SelectToken("date_of_birth"), out birthday);
            this.Sex = (string)token.SelectToken("gender");
            this.FamilyIncome = (float)token.SelectToken("family_home_info.family_monthly_income_usd");
            this.Siblings = (int)token.SelectToken("family_home_info.number_of_siblings");
            this.Subject = GetArray(token, "subjects");
            this.Hobbies = GetArray(token, "hobbies");
            this.Image = ImageSwitch((string)token.SelectToken("profile_picture"), organization);


            Console.WriteLine(this.Name);
        }
        public static Child CreateChild(JToken token, Organization organization)
        {
            return new Child(token, organization);
        }
        #endregion
        #region Methods
        private string ImageSwitch(string id, Organization organization)
        {
            return organization == Organization.HopeFoundation ? "https://students.hope.indibits.com/uploads/images/" + id + "_r.jpg" : "https://students.ogh.indibits.com/uploads/images/" + id + "_r.jpg";
        }
        private string SchoolSwitch(int id, Organization organization)
        {
            return organization == Organization.HopeFoundation ? HopeSchools[id] : id.ToString();
        }
        private string GradeSwitch(int id, Organization organization)
        {
            return organization == Organization.HopeFoundation ? HopeGrades[id] : id.ToString();
        }
        private string GetArray(JToken token, string id)
        {
            JArray array = (JArray)token.SelectToken(id);
            StringBuilder stringBuilder = new StringBuilder();
            foreach(JToken jtoken in array)
            {
                if (stringBuilder.Length <= 0)
                {
                    stringBuilder.Append((string)jtoken.SelectToken("name"));
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
        #region Enums
        public enum Organization
        {
            HopeFoundation,
            Ogh
        }
        #endregion
        #region Static Fields
        private static string[] HopeSchools = { "New Life Academy", "Good News Academy", "Valerye McMillian" };
        private static string[] HopeGrades = { "Preschool", "Kindergarten", "Kindergarten", "Kindergarten", "First", "Second", "Third", "Fourth", "Fifth", "Sixth", "Seventh", "Eight", "Freshman", "Sophomore", "Junior", "Senior" };
        private static string[] OghSchools = { };
        private static string[] OghGrades = { };
        #endregion
    }
}
