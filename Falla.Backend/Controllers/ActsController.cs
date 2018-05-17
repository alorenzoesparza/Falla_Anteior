using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
namespace Falla.Backend.Controllers
{
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Falla.Backend.Models;
    using Falla.Domain;

    public class ActsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Acts
        public async Task<ActionResult> Index()
        {
            return View(await db.Acts.ToListAsync());
        }

        // GET: Acts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Act act = await db.Acts.FindAsync(id);
            if (act == null)
            {
                return HttpNotFound();
            }
            return View(act);
        }

        // GET: Acts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Acts/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Act act)
        {
            if (ModelState.IsValid)
            {
                db.Acts.Add(act);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(act);
        }

        // GET: Acts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Act act = await db.Acts.FindAsync(id);
            if (act == null)
            {
                return HttpNotFound();
            }
            return View(act);
        }

        // POST: Acts/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "IdAct,Titular,Descripcion,FechaActo,HoraActo,Precio,PrecioInfantiles,ActoOficial,Imagen,Imagen500,PagInicio")] Act act)
        public async Task<ActionResult> Edit(Act act)
        {
            if (ModelState.IsValid)
            {
                db.Entry(act).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(act);
        }

        // GET: Acts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Act act = await db.Acts.FindAsync(id);
            if (act == null)
            {
                return HttpNotFound();
            }
            return View(act);
        }

        // POST: Acts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Act act = await db.Acts.FindAsync(id);
            db.Acts.Remove(act);
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
