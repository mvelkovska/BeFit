using System;
using System.Collections.Generic;
using System.Text;
using TryBeingFit.Domain.Enums;

namespace TryBeingFit.Domain.Models
{
    public abstract class User : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
         
        public UserRole UserRole { get; set; }


    }
}
