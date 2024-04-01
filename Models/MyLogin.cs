﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class MyLogin
    {
        [Required(ErrorMessage = "This field is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        public string Password { get; set;}
    }
}