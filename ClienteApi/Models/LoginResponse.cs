using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteApi.Models
{
    public class LoginResponse
    {
        public bool? Status { get; set; } = null;
        public string? Token { get; set; } = null;
    }
}
