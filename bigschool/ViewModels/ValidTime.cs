using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace bigschool.ViewModels
{
    public class ValidTime: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;
            var IsValid = DateTime.TryParseExact(Convert.ToString(value), "Hh:mm", CultureInfo.CurrentCulture, DateTimeStyles.None, out dateTime);
            return (IsValid && dateTime > DateTime.Now);
        }
    }
}