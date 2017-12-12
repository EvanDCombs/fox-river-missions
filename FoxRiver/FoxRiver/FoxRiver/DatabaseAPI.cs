using System;
using System.Collections.Generic;
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
        public static async Task<List<Child>> GetChildrenAsync()
        {
            if (children == null)
            {
                DatabaseAPI api = new DatabaseAPI();
                children = await api.RetrieveChildrenAsync();
            }
            return children;
        }
        #endregion
        #region Const Fiels
        #endregion
        #region Initialization
        private DatabaseAPI() { }
        #endregion
        #region Methods
        private async Task<List<Child>> RetrieveChildrenAsync()
        {
            List<Child> children = new List<Child>();

            children.AddRange(await RequestChildrenAsync("URL"));

            return children;
        }
        private async Task<List<Child>> RequestChildrenAsync(string url)
        {
            string json = await RetrieveJsonDataAsync(url);
            return await ParseJsonAsync(json);
        }
        private async Task<List<Child>> ParseJsonAsync(string json)
        {
            List<Child> children = new List<Child>();

            JObject jsonObject = JObject.Parse(json);
            JToken token = jsonObject.First;
            while (token != null)
            {
                children.Add(new Child(token));
                token = token.Next;
            }

            return children;
        }
        private async Task<string> RetrieveJsonDataAsync(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;
            request.Headers.Add("X-Authorization:" + "API_KEY");
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
    }
}
