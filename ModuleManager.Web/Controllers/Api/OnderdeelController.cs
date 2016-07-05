using System.Collections.Generic;
using System.Web.Http;
using ModuleManager.Domain;
using ModuleManager.Domain.Interfaces;
using ModuleManager.Web.Controllers.Api.Interfaces;

namespace ModuleManager.Web.Controllers.Api
{
    public class OnderdeelController : ApiController, IGenericApiController<Onderdeel> // TODO: replace-all 'Onderdeel' => 'Onderdeel'
    {
        private readonly IGenericRepository<Onderdeel> _onderdeelRepository;

        public OnderdeelController(IGenericRepository<Onderdeel> onderdeelRepository)
        {
            _onderdeelRepository = onderdeelRepository;
        }

        [HttpGet, Route("api/Onderdeel/Get")]
        public IEnumerable<Onderdeel> GetAll()
        {
            return _onderdeelRepository.GetAll();
        }

        [HttpGet, Route("api/Onderdeel/Get/{key}")]
        //public Onderdeel GetOne(string key)
        public Onderdeel GetOne(string schooljaar, string key)
        {
            var keys = new object[] { key };
            return _onderdeelRepository.GetOne(keys);
        }

        [HttpPost, Route("api/Onderdeel/Delete")]
        public void Delete(Onderdeel entity)
        {
            _onderdeelRepository.Delete(entity);
        }

        [HttpPost, Route("api/Onderdeel/Edit")]
        public void Edit(Onderdeel entity)
        {
            _onderdeelRepository.Edit(entity);
        }

        [HttpPost, Route("api/Onderdeel/Create")]
        public void Create(Onderdeel entity)
        {
            _onderdeelRepository.Create(entity);
        }
    }
}