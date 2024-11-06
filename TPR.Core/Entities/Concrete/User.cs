using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPR.Core.Entities;

namespace TPR.Core.Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string? ProfileImage { get; set; }
        public DateTime Birthdate { get; set; }
        public bool? Gender { get; set; }
        public string? Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Status { get; set; }
    }
}
