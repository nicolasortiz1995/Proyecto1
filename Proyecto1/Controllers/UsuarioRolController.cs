using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto1.Models;
using System.Web.Security;

namespace Proyecto1.Controllers
{
    public class UsuarioRolController : Controller
    {
        [Authorize]
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

        //Crear-Mostrar info
        public ActionResult Create()
        {
            return View();
        }

        //Crear-Receive
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(usuariorol usuariorol)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.usuariorol.Add(usuariorol);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error" + ex);
                return View();
            }
        }

        //Editar-Mostrar info
        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    usuariorol finduser = db.usuariorol.Where(a => a.id == id).FirstOrDefault();
                    return View(finduser);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }

        }

        //Editar info
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(usuariorol usuarioRolEdit)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    usuariorol usuarioRol = db.usuariorol.Find(usuarioRolEdit.id);

                    usuarioRol.idUsuario = usuarioRolEdit.idUsuario;
                    usuarioRol.idRol = usuarioRolEdit.idRol;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }

        }

        //Details-Mostrar info
        public ActionResult Details(int id)
        {
            //abriendo conexion a la BD
            using (var db = new inventario2021Entities())
            {
                //buscar usuario por id
                usuariorol user = db.usuariorol.Find(id);
                return View(user);
            }
        }

        //Delete
        public ActionResult Delete(int id)
        {
            using (var db = new inventario2021Entities())
            {
                var usuarioRol = db.usuariorol.Find(id);
                db.usuariorol.Remove(usuarioRol);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}