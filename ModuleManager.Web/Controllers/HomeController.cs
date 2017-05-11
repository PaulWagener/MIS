using ModuleManager.Domain;
using ModuleManager.Domain.Interfaces;
using ModuleManager.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ModuleManager.Web.Controllers
{

    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult Index()
        {

            var vm = new PortalVM();

            if(User != null)
            {
                var user = _unitOfWork.Context.User
                    .Include("Docents.Modules")
                    .Include("Docents.Modules_Eigenaar")
                    .FirstOrDefault(u => u.UserNaam == User.Identity.Name);

                if (user == null)
                    return new HttpUnauthorizedResult(); //bad login

                vm.User = user;

                vm.Schooljaar = _unitOfWork.Context.Schooljaren.OrderByDescending(sj => sj.JaarId).FirstOrDefault().JaarId;

                vm.ModulesContributed = new List<Module>();

                user.Docents.ToList()
                    .ForEach(d => d.Modules.Where(m => m.Schooljaar == vm.Schooljaar).ToList()
                    .ForEach(m => vm.ModulesContributed.Add(m)));

                vm.ModulesOwned = new List<Module>();
                user.Docents.ToList()
                    .ForEach(d => d.Modules_Eigenaar.Where(m => m.Schooljaar == vm.Schooljaar).ToList()
                    .ForEach(m => vm.ModulesOwned.Add(m)));

                    
            }

            return View(vm);
        }

        [HttpGet, Route("Home/Login")]
        public ActionResult Login()
        {
            return View();
        }

    }
}