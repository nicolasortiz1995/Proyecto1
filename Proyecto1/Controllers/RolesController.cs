using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto1.Models;

namespace Proyecto1.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.roles.ToList());
            }
        }
        
        //Crear-Mostrar info
        public ActionResult Create()
        {
            return View();
        }

        //Crear info
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(roles roles)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.roles.Add(roles);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error " + ex);
                return View();
            }
        }

        //Editar-Mostrar info
        public ActionResult Edit (int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    roles findRoles = db.roles.Where(a => a.id == id).FirstOrDefault();
                    return View(findRoles);
                }
            }catch(Exception ex)
            {
                ModelState.AddModelError("", "Error " + ex);
                return View();
            }
        }

        //Editar info
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (roles rolesEsdit)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    roles rol = db.roles.Find(rolesEsdit);
                    rol.descripcion = rolesEsdit.descripcion;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }catch(Exception ex)
            {
                ModelState.AddModelError("", "Error " + ex);
                return View();
            }
        }

        //Details-Mostrar info
        public ActionResult Details (int id)
        {
            using ( var db = new inventario2021Entities())
            {
                roles rol = db.roles.Find(id);
                return View(rol);
            }
        }

        //Delete
        public ActionResult Delete(int id)
        {
            using (var db = new inventario2021Entities())
            {
                var rol = db.roles.Find(id);
                db.roles.Remove(rol);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}