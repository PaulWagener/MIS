using System.Web.Http;
using ModuleManager.Domain;
using ModuleManager.Web.ViewModels.PartialViewModel;
using ModuleManager.Web.ViewModels.RequestViewModels;

namespace ModuleManager.Web.Controllers.Api.Interfaces
{
    public interface IModuleApiController
    {
        ModuleListViewModel GetOverview([FromBody] ArgumentsViewModel value);
        Module GetOne(string schooljaar, string key);
        void Delete(Module entity);
        void Create(Module entity);  
    }
}