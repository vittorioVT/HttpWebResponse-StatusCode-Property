using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TitleUrlResponse1.Models;

namespace TitleUrlResponse1.Controllers
{
    public class HomeController : Controller
    {

        Info info = new Info();
        public string url, address, title, responseContent;

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About(string text)
        {
            if (GetCorrectUrl(text) == null)
            {
                ViewBag.MessageAboutUrl = "Пожалуйста, введите корректный адрес";
            }
            else
            {
                url = GetCorrectUrl(text);

                HttpWebRequest httpWebReq = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse httpWebResp = (HttpWebResponse)httpWebReq.GetResponse();
                using (StreamReader streamReader = new StreamReader(httpWebResp.GetResponseStream()))
                {
                    responseContent = streamReader.ReadToEnd(); 
                }
                address = responseContent;
                            
                title = Regex.Match(address, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;
                ViewBag.TitleUrl = title;

            }
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
            // определяем title url-страницы
            if (GetCorrectUrl(text) == null)
            {
                ViewBag.MessageAboutUrl = "Пожалуйста, введите корректный адрес";
            }
            else
            {
                url = GetCorrectUrl(text);
            }
             url = GetCorrectUrl(text);
            address = new WebClient().DownloadString(url);
            string title = Regex.Match(address, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;

            //проверяем кодировку источника 

            return title;
            
            
            //string[] urls = text.Split(' ', ',', '.', '\t');
            //return urls;
        }

        //public string[] GetStatusCode(string text)
        //{
        //    List<Info> collectionList = new List<Info>()
        //    {
        //        new Info{ Address="", Title="", LastRequest=, LastStatus="", RequestCount= },
        //        new Info{ Address="", Title="", LastRequest=, LastStatus="", RequestCount= },
        //        new Info{ Address="", Title="", LastRequest=, LastStatus="", RequestCount= },
        //        new Info{ Address="", Title="", LastRequest=, LastStatus="", RequestCount= }
        //    };

        //    //return collectionList;

        //}




        





    }
}