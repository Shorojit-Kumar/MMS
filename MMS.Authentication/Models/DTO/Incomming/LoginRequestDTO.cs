﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Authentication.Models.DTO.Incomming
{
    public class LoginRequestDTO
    {
       
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}