using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaranSoft.MyGarage.Services.Models
{
    public class User
    {
        public string Nickname { get; set; }
        public string Email { get; set; }
        public Guid Id { get; set; }
        public string Password { get; set; }
    }
}
