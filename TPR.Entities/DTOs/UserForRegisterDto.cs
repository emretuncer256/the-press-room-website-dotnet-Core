using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPR.Core.Entities;

namespace TPR.Entities.DTOs
{
    public class UserForRegisterDto : IDto
    {
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public string Password { get; set; }
        public string RetypePassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
