using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgendaTelefonica.Controllers
{
    public class PersonaFormController : Controller
    {
        // GET: PersonaForm
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult RegistrarPersona()
        {
            return View();
        }
        [Authorize]
        public ActionResult VerPersonas()
        {
            return View();
        }

    }
}