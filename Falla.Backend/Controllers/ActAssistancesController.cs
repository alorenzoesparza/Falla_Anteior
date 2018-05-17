namespace Falla.Backend.Controllers
{
    using Falla.Backend.Models;
    using Falla.Domain;
    using System.Data.Entity;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class ActAssistancesController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: ActAssistances
        public async Task<ActionResult> Index()
        {
            return View(await db.ActAssistances.ToListAsync());
        }

        // GET: ActAssistances/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActAssistance actAssistance = await db.ActAssistances.FindAsync(id);
            if (actAssistance == null)
            {
                return HttpNotFound();
            }
            return View(actAssistance);
        }

        // GET: ActAssistances/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActAssistances/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdAsistencia,IdAct,IdFallero")] ActAssistance actAssistance)
        {
            if (ModelState.IsValid)
            {
                db.ActAssistances.Add(actAssistance);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(actAssistance);
        }

        // GET: ActAssistances/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActAssistance actAssistance = await db.ActAssistances.FindAsync(id);
            if (actAssistance == null)
            {
                return HttpNotFound();
            }
            return View(actAssistance);
        }

        // POST: ActAssistances/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdAsistencia,IdAct,IdFallero")] ActAssistance actAssistance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actAssistance).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(actAssistance);
        }

        // GET: ActAssistances/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActAssistance actAssistance = await db.ActAssistances.FindAsync(id);
            if (actAssistance == null)
            {
                return HttpNotFound();
            }
            return View(actAssistance);
        }

        // POST: ActAssistances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ActAssistance actAssistance = await db.ActAssistances.FindAsync(id);
            db.ActAssistances.Remove(actAssistance);
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
