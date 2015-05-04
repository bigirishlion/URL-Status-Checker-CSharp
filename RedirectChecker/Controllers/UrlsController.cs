using RedirectChecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace RedirectChecker.Controllers
{
    public class UrlsController : ApiController
    {
        // Returns the entire HTML
        //[HttpPost]
        //public async Task<string> Post([FromBody]MyUrl url) 
        //{
        //    using (var httpClient = new HttpClient())
        //        return await httpClient.GetStringAsync(url.Url);
        //}

        public string Post([FromBody]MyUrl url)
        {
            //return "Returned URL: " + url.Url;
            if (url.Url != null)
            {
                try
                {
                    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url.Url);
                    webRequest.AllowAutoRedirect = false;
                    webRequest.Method = "HEAD";
                    HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                    return "<tr><td>" + url.Url + "</td><td>" + (int)response.StatusCode + "</td></tr>";
                }
                catch (WebException ex)
                {
                    string s = "";
                    HttpWebResponse webResponse = (HttpWebResponse)ex.Response;
                    if (webResponse.StatusCode == HttpStatusCode.NotFound)
                    {
                        s = "<tr><td>" + url.Url + "</td><td>404</td></tr>";
                    }
                    return s;
                }
            }
            else
            {
                return "";
            }
        }
    }
}
