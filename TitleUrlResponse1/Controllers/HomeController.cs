using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Data.Entity;
using TitleUrlResponse1.Models;
using System.Threading.Tasks;

namespace TitleUrlResponse1.Controllers
{
    public class HomeController : Controller
    {
        Info info = new Info();
        public string url, address, title, responseContent;
        HttpStatusCode statusCode;
        DateTime lastModified;
        List<Info> InfoCollection = new List<Info>();
       
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> About(string text)
        {
            string[] arrays = text.Trim().Split(' ', ',', '\r', '\n');
            string[] urls = arrays.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            
            for (int i = 0; i < urls.Length; i++)
            {
                if (GetCorrectUrl(urls[i]) == null)
                {
                    ViewBag.MessageAboutUrl = "Пожалуйста, введите корректный адрес";
                }
                else
                {
                    url = GetCorrectUrl(urls[i]);
                    HttpWebRequest httpWebReq = (HttpWebRequest)WebRequest.Create(url);
                    HttpWebResponse httpWebResp = (HttpWebResponse)httpWebReq.GetResponse();
                    using (StreamReader streamReader = new StreamReader(httpWebResp.GetResponseStream()))
                    {
                        responseContent = await streamReader.ReadToEndAsync();
                        lastModified = httpWebResp.LastModified;
                        statusCode = httpWebResp.StatusCode;
                    }

                    address = responseContent;
                    title = Regex.Match(address, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;

                    InfoCollection.Add(new Info() { NameUrl = url, TitleUrl = title, StatusCode = statusCode, LastModified = lastModified });
                }
            }

             ViewBag.Info = InfoCollection;

            return View();
        }
       

        // проверяем, корректен ли адрес, который ввел пользователь
        public string GetCorrectUrl(string text)
        {
            if (text != null)
            {
                if (Regex.IsMatch(text, @"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$", RegexOptions.IgnoreCase))
                {
                    return text;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        //меняем кодировку title страницы 
        public string ConvertEncoding(string content)
        {
            string utfLine = content;
            Encoding utf = Encoding.UTF8;
            Encoding win = Encoding.GetEncoding(1251);
            byte[] utfArr = utf.GetBytes(utfLine);
            byte[] winArr = Encoding.Convert(win, utf, utfArr);
            string result = win.GetString(winArr);

            return result;
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        

        [HttpPost]
        public string GetUrl(string text)
        {
             return null;
        }
    }
}