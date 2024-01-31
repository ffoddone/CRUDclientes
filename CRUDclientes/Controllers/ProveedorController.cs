using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CRUDclientes.Models;

namespace CRUDclientes.Controllers
{
    public class ProveedorController : Controller
    {
        private static string conexion = ConfigurationManager.ConnectionStrings["cadena"].ToString();

        private static List<proveedor> olista = new List<proveedor>();

        // GET: Clientes

        public ActionResult Inicio()
        {
            olista = new List<proveedor>();

            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Proveedores", oconexion);

                cmd.CommandType = CommandType.Text; 
                oconexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        proveedor Nuevoproveedor = new proveedor();

                        Nuevoproveedor.Id_Proveedores = Convert.ToInt32(dr["Id_Proveedores"]);
                        Nuevoproveedor.Razon_Social = dr["Razon_Social"].ToString();
                        Nuevoproveedor.Sector_Comercial = dr["Sector_Comercial"].ToString();
                        Nuevoproveedor.RUC = dr["RUC"].ToString();
                        Nuevoproveedor.Direccion = dr["Direccion"].ToString();
                        Nuevoproveedor.Telefono = dr["Telefono"].ToString();
                        Nuevoproveedor.Email = dr["Email"].ToString();
                        Nuevoproveedor.URL = dr["URL"].ToString();

                        olista.Add(Nuevoproveedor);
                    }
                }
            }
                return View(olista);
        }

        [HttpGet]

        public ActionResult Registrar()
        {         
            return View();
        }
        [HttpGet]

        public ActionResult Editar(int? Id_proveedores)
        {
            if (Id_proveedores == null)
                return RedirectToAction("Inicio","proveedor");

            proveedor oproveedor = olista.Where(c=> c.Id_Proveedores == Id_proveedores).FirstOrDefault();

            return View(oproveedor);
        }    
        [HttpGet]

        public ActionResult Eliminar(int? Id_proveedores)
        {
            if (Id_proveedores == null)
                return RedirectToAction("Inicio","proveedor");

            proveedor oproveedor = olista.Where(c => c.Id_Proveedores == Id_proveedores).FirstOrDefault();

            return View(oproveedor);
        }

        [HttpPost]

        public ActionResult Registrar(proveedor oproveedor)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Registrar", oconexion);
                cmd.Parameters.AddWithValue("Razon_Social", oproveedor.Razon_Social);
                cmd.Parameters.AddWithValue("Sector_Comercial", oproveedor.Sector_Comercial);
                cmd.Parameters.AddWithValue("RUC", oproveedor.RUC);
                cmd.Parameters.AddWithValue("Direccion", oproveedor.Direccion);
                cmd.Parameters.AddWithValue("Telefono", oproveedor.Telefono);
                cmd.Parameters.AddWithValue("Email", oproveedor.Email);
                cmd.Parameters.AddWithValue("URL", oproveedor.URL);
                cmd.CommandType = CommandType.StoredProcedure;
                oconexion.Open();
                cmd.ExecuteNonQuery();            
            }

            return RedirectToAction("Inicio", "proveedor");
        }

        [HttpPost]

        public ActionResult Editar(proveedor oproveedor)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Editar", oconexion);
                cmd.Parameters.AddWithValue("Id_Proveedores", oproveedor.Id_Proveedores);
                cmd.Parameters.AddWithValue("Razon_Social", oproveedor.Razon_Social);
                cmd.Parameters.AddWithValue("Sector_Comercial", oproveedor.Sector_Comercial);
                cmd.Parameters.AddWithValue("RUC", oproveedor.RUC);
                cmd.Parameters.AddWithValue("Direccion", oproveedor.Direccion);
                cmd.Parameters.AddWithValue("Telefono", oproveedor.Telefono);
                cmd.Parameters.AddWithValue("Email", oproveedor.Email);
                cmd.Parameters.AddWithValue("URL", oproveedor.URL);
                cmd.CommandType = CommandType.StoredProcedure;
                oconexion.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Inicio", "proveedor");
        }

        [HttpPost]

        public ActionResult Eliminar(string Id_Proveedores)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Eliminar", oconexion);
                cmd.Parameters.AddWithValue("Id_Proveedores", Id_Proveedores);
                cmd.CommandType = CommandType.StoredProcedure;
                oconexion.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Inicio", "proveedor");
        }
    }
}