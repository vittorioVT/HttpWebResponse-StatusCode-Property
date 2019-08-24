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
        [UrlValidator(ErrorMessage = "please_enter_valid_ftp_url")]
        public string Address { get; set; }

    }

    public class UrlValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var x = value.ToString();
                if (Regex.IsMatch(x, @"^http:\/\/\w+(\.\w+)*(:[0-9]+)?\/?(\/[.\w]*)*$", RegexOptions.IgnoreCase))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            else
            {
                return new ValidationResult("Please enter some value");
            }
        }
    }

}