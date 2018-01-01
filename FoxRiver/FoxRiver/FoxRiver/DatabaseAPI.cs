using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FoxRiver
{
    public class DatabaseAPI
    {
        #region Static
        private static List<Child> children;
        public static async Task<List<Child>> GetChildrenAsync(IProgress<Child> progress)
        {
            if (children == null)
            {
                DatabaseAPI api = new DatabaseAPI();
                children = new List<Child>();
                children = await api.RetrieveChildrenListAsync(progress);
            }
            return children;
        }
        public static async Task<List<Child>> GetChildrenAsync()
        {
            if (children == null)
            {
                DatabaseAPI api = new DatabaseAPI();
                children = await api.RetrieveChildrenListAsync();
            }
            return children;
        }
        #endregion
        #region Const Fiels
        private const string HOPE_ACTIVE_URL = "http://students.hope.indibits.com/api/students?sponsored=false&status=Active";
        private const string HOPE_API_KEY = "44864d9389298d9b23b38c9d203715ae94fa58a3";

        private const string OGH_ACTIVE_URL = "https://students.ogh.indibits.com/api/students?sponsored=false&status=Active";
        private const string OGH_API_KEY = "071d1b4db2c50efb76fee0a2bfb1e3da12b61251";
        #endregion
        #region Delegates
        private delegate Child FilterChildren(JToken token);
        #endregion
        #region Initialization
        private DatabaseAPI() { }
        #endregion
        #region Methods
        private async Task<List<Child>> RetrieveChildrenListAsync()
        {
            List<Child> children = new List<Child>();

            
            children.AddRange(await RequestChildrenAsync(HOPE_ACTIVE_URL, HOPE_API_KEY, HopeActiveFilter));
            children.AddRange(await RequestChildrenAsync(OGH_ACTIVE_URL, OGH_API_KEY, OghFilter));

            children.Shuffle();
            return children;
        }
        private async Task<List<Child>> RetrieveChildrenListAsync(IProgress<Child> progress)
        {
            List<Child> children = new List<Child>();

            RequestChildrenAsync(HOPE_ACTIVE_URL, HOPE_API_KEY, (token) => {
                Child child = HopeActiveFilter(token);
                DatabaseAPI.children.Add(child);
                progress.Report(child);
                return child;
            });
            RequestChildrenAsync(OGH_ACTIVE_URL, OGH_API_KEY, (token) => {
                Child child = OghFilter(token);
                DatabaseAPI.children.Add(child);
                progress.Report(child);
                return child;
            });

            return children;
        }
        private async Task<List<Child>> RequestChildrenAsync(string url, string apiKey, FilterChildren filter)
        {
            string json = await RetrieveJsonDataAsync(url, apiKey);
            return ParseJson(json, filter);
        }
        private List<Child> ParseJson(string json, FilterChildren filter)
        {
            List<Child> list = new List<Child>();

            JObject jsonObject = JObject.Parse(json);
            JToken token = jsonObject.First;
            while (token != null)
            {
                Child child = filter(token);
                if (child != null)
                {
                    list.Add(child);
                }
                token = token.Next;
            }

            return list;
        }
        private async Task<string> RetrieveJsonDataAsync(string url, string apiKey)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;
            request.Headers.Add("X-Authorization:" + apiKey);
            request.ContentType = "application/json";

            try
            {
                WebResponse response = await request.GetResponseAsync();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return await reader.ReadToEndAsync();
                    }
                }
            }
            catch (WebException e)
            {
                using (StreamReader reader = new StreamReader(e.Response.GetResponseStream()))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }
        #endregion
        #region Child Filters
        private Child HopeStandbyFilter(JToken token)
        {
            int grade = (int)token.First.SelectToken("class_id");
            if (grade == 1)
            {
                return Child.CreateChild(token, Child.Organization.HopeFoundation);
            }
            return null;
        }
        private Child HopeActiveFilter(JToken token)
        {
            int school = (int)token.First.SelectToken("school_id");
            int grade = (int)token.First.SelectToken("class_id");
            if (school == 2 && grade >= 2)
            {
                return Child.CreateChild(token, Child.Organization.HopeFoundation);
            }
            else if (school == 3 && grade == 12)
            {
                int primarySchool = (int)token.First.SelectToken("primary_school");
                if (primarySchool == 3)
                {
                    return Child.CreateChild(token, Child.Organization.HopeFoundation);
                }
            }
            return null;
        }
        private Child OghFilter(JToken token)
        {
            return Child.CreateChild(token, Child.Organization.Ogh);
        }
        #endregion
    }
}
