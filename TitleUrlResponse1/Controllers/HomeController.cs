using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        
        [HttpGet]
        public ActionResult Index()
        {


            return View();
        }

        [HttpPost]
        public string GetUrl(string text)
        {
            // определяем title url-страницы
            string url = text;
            string address = new WebClient().DownloadString(url);
            string title = Regex.Match(address, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;

            //проверяем кодировку источника 

            return title;
            //string[] urls = text.Split(' ', ',', '.', '\t');
            //return urls;
        }
    }
}