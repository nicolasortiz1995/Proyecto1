using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto1.Models;
using Rotativa;

namespace Proyecto1.Controllers
{
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.proveedor.ToList());
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
        public ActionResult Create(proveedor proveedor)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.proveedor.Add(proveedor);
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
                    proveedor findProveedor = db.proveedor.Where(a => a.id == id).FirstOrDefault();
                    return View(findProveedor);
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
        public ActionResult Edit(proveedor proveedorEdit)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    proveedor Provee = db.proveedor.Find(proveedorEdit.id);
                    Provee.nombre = proveedorEdit.nombre;
                    Provee.direccion = proveedorEdit.direccion;
                    Provee.telefono = proveedorEdit.telefono;
                    Provee.nombre_contacto = proveedorEdit.nombre_contacto;
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
            using (var db = new inventario2021Entities())
            {
                proveedor Provee = db.proveedor.Find(id);
                return View(Provee);
            }
        }

        //Delete
        public ActionResult Delete(int id)
        {
            using (var db = new inventario2021Entities())
            {
                var Provee = db.proveedor.Find(id);
                db.proveedor.Remove(Provee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
       
        public ActionResult uploadCSV2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult uploadCSV2 (HttpPostedFileBase fileForm)
        {
            //String para guardar la ruta.
            string filePath = string.Empty;

            //Condición para saber si llegó el archivo.
            if (fileForm != null)
            {
                //Rute de la carpeta que guardará el archivo
                string path = Server.MapPath("~/Uploads/");

                //Condición para saber si la ruta de la carpeta existe.
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //Obtener el nombre del archivo.
                filePath = path + Path.GetFileName(fileForm.FileName);
                //Obtener la extensión del archivo.
                string extension = Path.GetExtension(fileForm.FileName);
                //Guardar el archivo.
                fileForm.SaveAs(filePath);

                string csvData = System.IO.File.ReadAllText(filePath);

                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        var newProveedor = new proveedor
                        {
                            nombre = row.Split(',')[0],
                            direccion = row.Split(',')[1],
                            telefono = row.Split(',')[2],
                            nombre_contacto = row.Split(',')[3],
                        };
                        using(var db = new inventario2021Entities())
                        {
                            db.proveedor.Add(newProveedor);
                            db.SaveChanges();
                        }
                    }
                }
            }
            return View();
        }
    }
}