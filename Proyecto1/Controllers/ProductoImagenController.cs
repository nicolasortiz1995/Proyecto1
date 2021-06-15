using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto1.Models;

namespace Proyecto1.Controllers
{
    public class ProductoImagenController : Controller
    {
        // GET: ProductoImagen
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.producto_imagen.ToList());
            }
        }


        public static string NProducto(int idNProducto)
        {
            using (var db = new inventario2021Entities())
            {
                return db.producto.Find(idNProducto).nombre;
            }
        }

        //Mostrar listar Producto
        public ActionResult ListarNProducto()
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
        public ActionResult Create(producto_imagen newProductoImagen)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.producto_imagen.Add(newProductoImagen);
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
                producto_imagen productoImagenDetalle = db.producto_imagen.Where(a => a.id == id).FirstOrDefault();
                return View(productoImagenDetalle);
            }
        }

        //Editar-Show
        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    producto_imagen productoImagen = db.producto_imagen.Where(a => a.id == id).FirstOrDefault();
                    return View(productoImagen);
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
        public ActionResult Edit(producto_imagen productoImagenEdit)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var productoImagen = db.producto_imagen.Find(productoImagenEdit.id);
                    productoImagen.imagen = productoImagenEdit.imagen;
                    productoImagen.id_producto = productoImagenEdit.id_producto;
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
                var productoImagenDelete = db.producto_imagen.Find(id);
                db.producto_imagen.Remove(productoImagenDelete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        

    }
}