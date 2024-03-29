﻿namespace Falla.Backend.Controllers
{
    using Falla.Backend.Models;
    using Falla.Domain;
    using System.Data.Entity;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.IO;
    using Helpers;

    [Authorize(Roles = "Admin, Fallero")]
    public class ComponentsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Components
        public async Task<ActionResult> Index()
        {
            return View(await db.Components.ToListAsync());
        }

        // GET: Components/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var component = await db.Components.FindAsync(id);

            if (component == null)
            {
                return HttpNotFound();
            }

            return View(component);
        }

        // GET: Components/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Components/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ComponentView view)
        {
            if (ModelState.IsValid)
            {
                var component = this.ToComponent(view);
                db.Components.Add(component);
                await db.SaveChangesAsync();
                UsersHelper.CreateUserASP(view.Email,"Fallero", view.Password);

                if (view.FotoFile != null)
                {
                    var folder = "~/Content/Componentes";
                    var file = string.Format("Acto{0}", component.ComponentId);
                    var file500 = string.Format("Acto{0}_{1}", component.ComponentId, "500");

                    var respuesta = FilesHelper.UploadPhotoBackEnd(view.FotoFile, folder, file, 200, 200);
                    var respuesta500 = FilesHelper.UploadPhotoBackEnd(view.FotoFile, folder, file500, 500, 500);

                    var extension = Path.GetExtension(respuesta);
                    if (respuesta != null)
                    {
                        var pic = string.Format("{0}/{1}{2}", folder, file, extension);
                        var pic500 = string.Format("{0}/{1}{2}", folder, file500, extension);

                        component.Foto = pic;
                        component.Foto500 = pic500;

                        db.Entry(component).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                return RedirectToAction("Index");
            }

            return View(view);
        }

        private Component ToComponent(ComponentView view)
        {
            return new Component
            {
                ComponentId = view.ComponentId,
                Email = view.Email,
                FirstName = view.FirstName,
                Foto = view.Foto,
                Foto500 = view.Foto500,
                LastName = view.LastName,
                Telephone = view.Telephone,
            };
        }

        // GET: Components/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var component = await db.Components.FindAsync(id);

            if (component == null)
            {
                return HttpNotFound();
            }

            return View(component);
        }

        // POST: Components/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "ComponentId,FirstName,LastName,Email,Telephone,ImagePath")] Component component)
        public async Task<ActionResult> Edit(Component component)
        {
            if (ModelState.IsValid)
            {
                //var component = this.ToComponent(view);
                if (component.FotoFile != null)
                {
                    var folder = "~/Content/Componentes";
                    var file = string.Format("Acto{0}", component.ComponentId);
                    var file500 = string.Format("Acto{0}_{1}", component.ComponentId, "500");

                    var respuesta = FilesHelper.UploadPhotoBackEnd(component.FotoFile, folder, file, 200, 200);
                    var respuesta500 = FilesHelper.UploadPhotoBackEnd(component.FotoFile, folder, file500, 500, 500);

                    var extension = Path.GetExtension(respuesta);
                    if (respuesta != null)
                    {
                        var pic = string.Format("{0}/{1}{2}", folder, file, extension);
                        var pic500 = string.Format("{0}/{1}{2}", folder, file500, extension);

                        component.Foto = pic;
                        component.Foto500 = pic500;
                    }
                }

                db.Entry(component).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(component);
        }

        // GET: Components/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Component component = await db.Components.FindAsync(id);
            if (component == null)
            {
                return HttpNotFound();
            }
            return View(component);
        }

        // POST: Components/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Component component = await db.Components.FindAsync(id);
            db.Components.Remove(component);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
