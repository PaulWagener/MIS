using System.Linq;
using System.Web.Mvc;
using ModuleManager.BusinessLogic.Data;
using ModuleManager.BusinessLogic.Interfaces.Services;
using ModuleManager.DomainDAL;
using ModuleManager.DomainDAL.Interfaces;
using ModuleManager.UserDAL.Interfaces;
using ModuleManager.Web.ViewModels;
using ModuleManager.Web.ViewModels.PartialViewModel;

namespace ModuleManager.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        private readonly IFilterSorterService<Module> _filterSorterService;

        public AdminController(IFilterSorterService<Module> filterSorterService, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _filterSorterService = filterSorterService;
        }

        [HttpGet, Route("Admin/Index")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet, Route("Admin/Curriculum")]
        public ActionResult Curriculum()
        {

            var schooljaren = _unitOfWork.GetRepository<Schooljaar>().GetAll().ToArray();
            var laatsteSchooljaar = schooljaren.Last();

            var arguments = new ModuleFilterSorterArguments
            {

            };
            
            /*var queryPack = new ModuleQueryablePack(arguments, _unitOfWork.GetRepository<Module>().GetAll().AsQueryable());
            var modules = _filterSorterService.ProcessData(queryPack).ToList();
            var moduleList = new ModuleListViewModel(modules.Count());
            moduleList.AddModules(modules);*/

            var competenties = _unitOfWork.GetRepository<Competentie>().GetAll().Where(x => x.Schooljaar == laatsteSchooljaar.JaarId).ToArray();
            var leerlijnen = _unitOfWork.GetRepository<Leerlijn>().GetAll().Where(x => x.Schooljaar == laatsteSchooljaar.JaarId).ToArray();
            var tags = _unitOfWork.GetRepository<Tag>().GetAll().ToArray();
            var fases = _unitOfWork.GetRepository<Fase>().GetAll().Where(x => x.Schooljaar == laatsteSchooljaar.JaarId).ToArray();
            var onderdelen = _unitOfWork.GetRepository<Onderdeel>().GetAll().ToArray();
            var docenten = _unitOfWork.GetRepository<Docent>().GetAll().ToArray();

            var blokken = _unitOfWork.GetRepository<Blok>().GetAll().ToArray();
            var modules = _unitOfWork.GetRepository<Module>().GetAll().Where(x => x.Schooljaar == laatsteSchooljaar.JaarId).ToArray();

            var filterOptions = new FilterOptionsViewModel();
            filterOptions.AddFases(fases);
            filterOptions.AddBlokken(blokken);

            var adminCurriculumVm = new AdminCurriculumViewModel
            {
                Competenties = competenties,
                Leerlijn = leerlijnen,
                Tags = tags,
                Fases = fases,
                //ModuleViewModels = moduleList,
                FilterOptions = filterOptions,
                Onderdeel = onderdelen,
                Modules = modules,
                Docenten = docenten
            };

            return View(adminCurriculumVm);
        }


        [HttpGet, Route("Admin/UserOverview")]
        public ActionResult UserOverview()
        {
            var userList = _userRepository.GetAll().ToArray();
            var usersListVm = new UserListViewModel(userList.Count());
            usersListVm.AddUsers(userList);

            var overViewvm = new AdminUserManagementViewModel()
            {
                Users = usersListVm
            };

            return View(overViewvm);
        }

        [HttpGet, Route("Admin/CheckModules")]
        public ActionResult CheckModules()
        {
            var arguments = new ModuleFilterSorterArguments
            {
                LeerjaarFilter = _unitOfWork.GetRepository<Schooljaar>().GetAll().Max(src => src.JaarId)
            };
            var maxSchooljaar = _unitOfWork.GetRepository<Schooljaar>().GetAll().Max(src => src.JaarId);
            var queryPack = new ModuleQueryablePack(arguments, _unitOfWork.GetRepository<Module>().GetAll().AsQueryable().Where(src => src.Schooljaar.Equals(maxSchooljaar)));
            var modules = _filterSorterService.ProcessData(queryPack).ToList();
            var moduleList = new ModuleListViewModel(modules.Count());
            moduleList.AddModules(modules);

            var users = _userRepository.GetAll().AsQueryable();
            var userList = new UserListViewModel(users.Count());
            userList.AddUsers(users);

            var checkModulesVm = new CheckModulesViewModel
            {
                ModuleViewModels = moduleList,
                Users = userList
            };

            return View(checkModulesVm);
        }

        [HttpGet, Route("Admin/Archive")]
        public ActionResult Archive()
        {
            return View();
        }

        [HttpPost, Route("Admin/Archive")]
        public ActionResult Archive(string code)
        {
            if (code != "ARCHIVEREN")
            {
                ViewBag.Message = "Invoer incorrect, probeer opnieuw.";
                return View();
            }

            //ViewBag.Message = "code: " + code;

            using (var context = new DomainContext())
            {
                context.SP_ArchiveYear();
            }

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}