using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace TitleUrlResponse1.Models
{
    public class Info
    {
        public string NameUrl { get; set; }
        public string TitleUrl { get; set; }
        public string StatusCode { get; set; }
        public DateTime LastModified { get; set; }
        public int RequestCount { get; set; }
    }
}