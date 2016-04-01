using System.Collections.Generic;
using System.Web.Http;
using ModuleManager.DomainDAL;
using ModuleManager.DomainDAL.Interfaces;
using ModuleManager.Web.Controllers.Api.Interfaces;

namespace ModuleManager.Web.Controllers.Api
{
    public class DocentController : ApiController
    {
        private readonly IGenericRepository<Docent> _docentRepository;

        public DocentController(IGenericRepository<Docent> docentRepository)
        {
            _docentRepository = docentRepository;
        }

        [HttpGet, Route("api/Docent/Get")]
        public IEnumerable<Docent> GetAll()
        {
            return _docentRepository.GetAll();
        }

        [HttpGet, Route("api/Docent/Get/{key}")]
        //public Onderdeel GetOne(string key)
        public Docent GetOne(int id)
        {
            var keys = new object[] { id };
            return _docentRepository.GetOne(keys);
        }

        [HttpPost, Route("api/Docent/Delete")]
        public void Delete(Docent entity)
        {
            _docentRepository.Delete(entity);
        }

        [HttpPost, Route("api/Docent/Edit")]
        public void Edit(Docent entity)
        {
            _docentRepository.Edit(entity);
        }

        [HttpPost, Route("api/Docent/Create")]
        public void Create(Docent entity)
        {
            _docentRepository.Create(entity);
        }
    }
}