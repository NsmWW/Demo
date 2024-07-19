using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DemoBTL.Domain.Validations
{
    public class VAlidateinput
    {
        public static bool isValiEmail(string Email)
        {
            var EmailAttribute = new EmailAddressAttribute();
            return EmailAttribute.IsValid(Email);
        }
        public static bool isValiPhone(string Phone)
        {
            string patern = @"^(0|\+84)(9\d|8[1-9]|7[06-9]|5[68]|3\d)\d{7}$";
            return Regex.IsMatch(Phone, patern);
        }

    }
}
