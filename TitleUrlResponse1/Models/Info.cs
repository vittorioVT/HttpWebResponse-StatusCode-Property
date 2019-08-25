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
        public string Address { get; set; }
        public string Title { get; set; }
        public string LastStatus { get; set; }
        public DateTime LastRequest { get; set; }
        public int RequestCount { get; set; }
    }

    

}