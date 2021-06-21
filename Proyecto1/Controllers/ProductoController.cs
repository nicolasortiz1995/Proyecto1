using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto1.Models;
using Rotativa;

namespace Proyecto1.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.producto.ToList());
            }            
        }


        public static string NombreProveedor(int idProveedor)
        {
            using (var db = new inventario2021Entities())
            {
                return db.proveedor.Find(idProveedor).nombre;
            }
        }

        //Mostrar listar proveedores
        public ActionResult ListarProveedores()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.proveedor.ToList());
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
        public ActionResult Create(producto newProducto)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.producto.Add(newProducto);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }catch(Exception ex)
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
                producto productoDetalle = db.producto.Where(a => a.id == id).FirstOrDefault();
                return View(productoDetalle);
            }
        }
        
        //Edit-Show
        public ActionResult Edit (int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    producto producto = db.producto.Where(a => a.id == id).FirstOrDefault();
                    return View(producto);
                }
            }catch(Exception ex)
            {
                ModelState.AddModelError("", "Erorr " + ex);
                return View();
            }
        }

        //Edit-Receive
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (producto productoEdit)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var producto = db.producto.Find(productoEdit.id);
                    producto.nombre = productoEdit.nombre;
                    producto.percio_unitario = productoEdit.percio_unitario;
                    producto.cantidad = productoEdit.cantidad;
                    producto.descripcion = productoEdit.descripcion;
                    producto.id_proveedor = productoEdit.id_proveedor;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }catch (Exception ex)
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
                var productDelete = db.producto.Find(id);
                db.producto.Remove(productDelete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }
}