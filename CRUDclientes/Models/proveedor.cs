using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDclientes.Models
{
    public class proveedor
    {
        public int Id_Proveedores { get; set; }

        public string Razon_Social { get; set; }

        public string Sector_Comercial { get; set; }

        public string RUC { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }

        public string URL { get; set; }

    }
}