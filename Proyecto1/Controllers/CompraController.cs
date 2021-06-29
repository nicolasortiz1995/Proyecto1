using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto1.Models;
using Rotativa;

namespace Proyecto1.Controllers
{
    public class CompraController : Controller
    {
        // GET: Compra
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.compra.ToList());
            }
        }

        public static string NombreUsuario(int idUsuario)
        {
            using (var db = new inventario2021Entities())
            {
                return db.usuario.Find(idUsuario).nombre;
            }
        }

        //Mostrar listar Usuario
        public ActionResult ListarUsuario()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.usuario.ToList());
            }
        }


        public static string NombreCliente(int idCliente)
        {
            using (var db = new inventario2021Entities())
            {
                return db.cliente.Find(idCliente).nombre;
            }
        }

        //Mostrar listar Cliente
        public ActionResult ListarCliente()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.cliente.ToList());
            }
        }

        //Create-Show
        public ActionResult Create()
        {
            return View();
        }

        //Create-Receive
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(compra newCompra)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.compra.Add(newCompra);
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

        //Details-Show
        public ActionResult Details(int id)
        {
            using (var db = new inventario2021Entities())
            {
                compra compraDetalle = db.compra.Where(a => a.id == id).FirstOrDefault();
                return View(compraDetalle);
            }
        }

        //Edit-Show
        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    compra compra = db.compra.Where(a => a.id == id).FirstOrDefault();
                    return View(compra);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error " + ex);
                return View();
            }
        }

        //Edit-Receive
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(compra compraEdit)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var compra = db.compra.Find(compraEdit.id);
                    compra.fecha = compraEdit.fecha;
                    compra.total = compraEdit.total;
                    compra.id_usuario = compraEdit.id_usuario;
                    compra.id_cliente = compraEdit.id_cliente;
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

        //Delete
        public ActionResult Delete(int id)
        {
            using (var db = new inventario2021Entities())
            {
                var compraDelete = db.compra.Find(id);
                db.compra.Remove(compraDelete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        //Generar reporte de compra
        public ActionResult ReporteCompra()
        {

            try
            {
                var db = new inventario2021Entities();
                var query = from tabCliente in db.cliente
                            join tabCompra in db.compra on tabCliente.id equals tabCompra.id_cliente
                            select new ReporteCompra
                            {
                                nombreCliente = tabCliente.nombre,
                                documentoCliente = tabCliente.documento,
                                fechaCompra = tabCompra.fecha,
                                totalCompra = tabCompra.total
                            };
                return View(query);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error" + ex);
                return View();
            }

        }

        public ActionResult ImprimirReporte()
        {
            var DateAndTime = DateTime.Now;
            return new ActionAsPdf("ReporteCompra") { FileName = "Reporte Compra_" + DateAndTime + ".pdf"  };
        }
    }
}