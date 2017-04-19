using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace QuestionsAndAnswers.Models
{
    public class CustomRangeDateValidationAttribute : RangeAttribute
    {
        public CustomRangeDateValidationAttribute(string minimumValue) : base(typeof(DateTime), minimumValue, DateTime.Now.ToShortDateString())
        {
            /*base keyword to invoke base class(Range Attribute) constructor
             * 
             * Constructor with empty body*/
        }
    }
    public class CurrentDateAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            DateTime dt = Convert.ToDateTime(value);
            if (dt <= DateTime.Now)
                return true;
            else
                return false;

            //  return dt <= DateTime.Now;
        }
    }
}