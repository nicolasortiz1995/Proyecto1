using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto1.Models;

namespace Ejemplo1aspnetmvc.Controllers
{
    public class UsuarioRolController : Controller
    {
        // GET: UsuarioRol
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.usuariorol.ToList());
            }
        }

        public static string NombreUsuarioRol(int idUsuarioRol)
        {
            using (var db = new inventario2021Entities())
            {
                return db.usuario.Find(idUsuarioRol).nombre;
            }
        }

        //Mostrar listas proveedores
        public ActionResult ListarUsuarioRol()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.usuario.ToList());
            }
        }

        public static string NombreRol(int idRol)
        {
            using (var db = new inventario2021Entities())
            {
                return db.roles.Find(idRol).descripcion;
            }
        }


        public ActionResult ListarRol()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.roles.ToList());
            }
        }


    }
}