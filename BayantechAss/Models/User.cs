﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace BayantechAss.Models
{
    public class User
    {
        public int ID { get; set; }


        [StringLength(50, MinimumLength = 7)]//Name must be between 7 and 50 characters
        [Required(ErrorMessage = "Enter Employee Name Please ^_^")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "Enter a valid Name please ^_^")]
         public string Name { get; set; }


        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Enter a valid Email please ^_^")]
        [Required(ErrorMessage = "Enter Employee Email Please ^_^")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 7)]//Password must be between 7 and 50 characters
        [Required(ErrorMessage = "Enter Employee Password Please ^_^")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter Employee Gender Please ^_^")]
        public string Gender { get; set; }

        [Display(Name = "Born in:")]
        [Required(ErrorMessage = "Enter Employee Birth Date Please ^_^")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [CustomRangeDateValidation("01/01/1954")]//it  works well,it accepts any date between 01/01/1954 and  CurrentMonth/CurrentDay/CurrentYear
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Enter Employee Address Please ^_^")]
        public string Address { get; set; }

        public static int returnUserID(string email) {
                     DBContextClass db = new DBContextClass();
                     User user = db.Users.Single(u => u.Email == email);
                     return user.ID;

        }

        public static string returnUserEmail(int UserID)
        {
            DBContextClass db = new DBContextClass();
            User user = db.Users.Single(u => u.ID == UserID);
            return user.Email;

        }
    }

}