using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Proyecto1.Models;
using System.Web.Security;

namespace Proyecto1.Controllers
{
    public class UsuarioController : Controller
    {
        [Authorize]
        // GET: Usuario
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.usuario.ToList());
            }
        }

        //Crear-Mostrar info
        public ActionResult Create ()
        {
            return View();
        }

        //Crear info
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(usuario usuario)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventario2021Entities())
                {
                    usuario.password = UsuarioController.HashSHA1(usuario.password);
                    db.usuario.Add(usuario);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }catch(Exception ex)
            {
                ModelState.AddModelError("","Error " + ex);
                return View();
            }
        }

        //Encriptacion
        public static string HashSHA1 (string value)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hash = sha1.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        //Editar-Mostrar info
        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    usuario findUser = db.usuario.Where(a => a.id == id).FirstOrDefault();
                    return View (findUser);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error " + ex);
                return View();
            }
        }

        //Editar info
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(usuario usuarioEdit)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventario2021Entities())
                {
                    usuario user = db.usuario.Find(usuarioEdit.id);
                    user.nombre = usuarioEdit.nombre;
                    user.apellido = usuarioEdit.apellido;
                    user.email = usuarioEdit.email;
                    user.fecha_nacimiento = usuarioEdit.fecha_nacimiento;
                    user.password = usuarioEdit.password;
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
                usuario user = db.usuario.Find(id);
                return View(user);
            }
        }

        //Login-Show
        public ActionResult Login(string message = "")
        {
            ViewBag.Message = message;
            return View();  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Login-Receive
        public ActionResult Login(string email, string password)
        {
            string passEncrip = UsuarioController.HashSHA1(password);
            using (var db = new inventario2021Entities())
            {
                var userLogin = db.usuario.FirstOrDefault(e => e.email == email && e.password == passEncrip);
                if (userLogin != null)
                {
                    FormsAuthentication.SetAuthCookie(userLogin.email, true);
                    Session["User"] = userLogin;
                    return RedirectToAction("Index");
                }
                else
                {
                    return Login("Verifique sus datos.");
                }
            }
        }

        //Logout
        [Authorize]
        public ActionResult CloseSession()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }

        //Delete
        public ActionResult Delete(int id)
        {
            using (var db = new inventario2021Entities())
            {
                var usuario = db.usuario.Find(id);
                db.usuario.Remove(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}