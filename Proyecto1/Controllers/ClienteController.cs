using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto1.Models;

namespace Proyecto1.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.cliente.ToList());
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
        public ActionResult Create(cliente cliente)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.cliente.Add(cliente);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }catch(Exception ex)
            {
                ModelState.AddModelError("", "Error " + ex);
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
                    cliente findCliente = db.cliente.Where(a => a.id == id).FirstOrDefault();
                    return View(findCliente);
                }
            }catch (Exception ex)
            {
                ModelState.AddModelError("", "Error " + ex);
                return View();
            }
        }

        //Editar info
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(cliente clienteEdit)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    cliente clien = db.cliente.Find(clienteEdit.id);
                    clien.nombre = clienteEdit.nombre;
                    clien.documento = clienteEdit.documento;
                    clien.email = clienteEdit.email;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }catch (Exception ex)
            {
                ModelState.AddModelError("", "Error " + ex);
                return View();
            }
        }

        //Details-Mostrar info
        public ActionResult Details (int id)
        {
            using (var db = new inventario2021Entities())
            {
                cliente clien = db.cliente.Find(id);
                return View(clien);
            }
        }

        //Delete
        public ActionResult Delete(int id)
        {
            using (var db = new inventario2021Entities())
            {
                var Clien = db.cliente.Find(id);
                db.cliente.Remove(Clien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}