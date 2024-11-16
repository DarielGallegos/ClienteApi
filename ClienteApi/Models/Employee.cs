using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteApi.Models
{
    public class Employee
    {
        public int? Id { get; set; } = null;
        public string? Nombre { get; set; } = null;
        public string? Fecha { get; set; } = null;
        public string? Cedula { get; set; } = null;
        public string? Telefono { get; set; } = null;
        public string? Direccion { get; set; } = null;
    }
}
