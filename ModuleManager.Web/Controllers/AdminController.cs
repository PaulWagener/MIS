using System.Linq;
using System.Web.Mvc;
using ModuleManager.BusinessLogic.Data;
using ModuleManager.BusinessLogic.Interfaces.Services;
using ModuleManager.Domain;
using ModuleManager.Domain.Interfaces;
using ModuleManager.UserDAL.Interfaces;
using ModuleManager.Web.ViewModels;
using ModuleManager.Web.ViewModels.PartialViewModel;
using System.Collections.Generic;

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
            if (TempData.ContainsKey("Messages"))
            {
                ViewBag.Messages = TempData["Messages"];
                ViewBag.MessageStatus = TempData["MessageStatus"];
            }
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

            var competenties = _unitOfWork.GetRepository<Competentie>().GetAll().ToArray();
            var leerlijnen = _unitOfWork.GetRepository<Leerlijn>().GetAll().ToArray();
            var tags = _unitOfWork.GetRepository<Tag>().GetAll().ToArray();
            var fases = _unitOfWork.GetRepository<Fase>().GetAll().ToList();
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
                LeerjaarFilter = _unitOfWork.Context.Schooljaren.Max(src => src.JaarId),
                Offset = 0,
                Limit = null
            };

            int totalNumberOfRecords;
            var modules = _filterSorterService.ProcessData(new ModuleQueryablePack(arguments, _unitOfWork.Context.Modules), out totalNumberOfRecords);
            var moduleList = new ModuleListViewModel(modules.Count(), modules);

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
            using (var context = new DomainEntities())
            {
                var jaren = context.Schooljaren.Select(sj => sj.JaarId).OrderByDescending(jaar => jaar).ToArray();
                return View(new AdminArchiverenViewModel()
                {
                    CopyCohort = true,
                    Cohorten = jaren,
                    CopyToCohort = jaren.First() + 101 // bijv. 1516 => 1617
                });
            }
        }

        [HttpPost, Route("Admin/Archive")]
        public ActionResult Archive(AdminArchiverenViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Er is geen geldig getal ingevuld bij het cohort. Probeer het alstublieft opnieuw.";
                return View(viewModel);
            }

            if (viewModel.BevestigingsCode != "ARCHIVEREN")
            {
                ViewBag.Message = "Invoer incorrect, probeer opnieuw.";
                return View(viewModel);
            }

            using (var context = new DomainEntities())
            {
                var messages = new List<string>();
                if (viewModel.CopyCohort)
                {
                    context.sp_CopyCohort(viewModel.TeArchiverenCohort, viewModel.CopyToCohort);
                    messages.Add(string.Format("Kopiëren van cohort {0} naar cohort {1} succesvol.", viewModel.TeArchiverenCohort, viewModel.CopyToCohort.Value));
                }

                messages.Add(string.Format("Archiveren van cohort {0} succesvol.", viewModel.TeArchiverenCohort));
                TempData["Messages"] = messages;
                TempData["MessageStatus"] = "success";
                context.spArchiveCohort(viewModel.TeArchiverenCohort);
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}