using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto1.Models;

namespace Proyecto1.Controllers
{
    public class ProductoCompraController : Controller
    {
        // GET: ProductoCompra
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.producto_compra.ToList());
            }
        }

        public static int NombreCompra(int idCompra)
        {
            using (var db = new inventario2021Entities())
            {
                return db.compra.Find(idCompra).id;
            }
        }

        //Mostrar listar Compra
        public ActionResult ListarCompra()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.compra.ToList());
            }
        }

        public static string NombreProducto(int idProducto)
        {
            using (var db = new inventario2021Entities())
            {
                return db.producto.Find(idProducto).nombre;
            }
        }

        //Mostrar listar producto
        public ActionResult ListarProducto()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.producto.ToList());
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
        public ActionResult Create(producto_compra newProductoCompra)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.producto_compra.Add(newProductoCompra);
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
                producto_compra productoCompraDetalle = db.producto_compra.Where(a => a.id == id).FirstOrDefault();
                return View(productoCompraDetalle);
            }
        }

        //Editar-Show
        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    producto_compra productoCompra = db.producto_compra.Where(a => a.id == id).FirstOrDefault();
                    return View(productoCompra);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error " + ex);
                return View();
            }
        }

        //Editar-Receive
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(producto_compra productoCompraEdit)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var productoCompra = db.producto_compra.Find(productoCompraEdit.id);
                    productoCompra.id_compra = productoCompraEdit.id_compra;
                    productoCompra.id_producto = productoCompraEdit.id_producto;
                    productoCompra.cantidad = productoCompraEdit.cantidad;
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
                var productoCompraDelete = db.producto_compra.Find(id);
                db.producto_compra.Remove(productoCompraDelete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }
}