using System;
using System.Net;

namespace TitleUrlResponse1.Models
{
    public class Info
    {
        public string NameUrl { get; set; }
        public string TitleUrl { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public DateTime LastModified { get; set; }
        public int RequestCount { get; set; }
    }
}