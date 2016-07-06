using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ModuleManager.Domain;
using ModuleManager.Domain.Interfaces;
using ModuleManager.Web.Controllers.Api.Interfaces;

namespace ModuleManager.Web.Controllers.Api
{
    public class FaseController : ApiController, IGenericApiController<Fase>
    {
        private readonly IUnitOfWork _unitOfWork;

        public FaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet, Route("api/Fase/Get")]
        public IEnumerable<Fase> GetAll()
        {
            var maxSchooljaar = _unitOfWork.GetRepository<Schooljaar>().GetAll().Max(src => src.JaarId);
            var fases = _unitOfWork.GetRepository<Fase>().GetAll().ToList();
            return fases;
        }

        [HttpGet, Route("api/Fase/Get/{schooljaar}/{key}")]
        public Fase GetOne(string schooljaar, string key)
        {
            var fase = _unitOfWork.GetRepository<Fase>().GetOne(new object[] { key, schooljaar });
            return fase;
        }

        [HttpPost, Route("api/Fase/Delete")]
        public void Delete(Fase entity)
        {
            _unitOfWork.GetRepository<Fase>().Delete(entity);
        }

        [HttpPost, Route("api/Fase/Edit")]
        public void Edit(Fase entity)
        {
            _unitOfWork.GetRepository<Fase>().Edit(entity);
        }

        [HttpPost, Route("api/Fase/Create")]
        public void Create(Fase entity)
        {
            _unitOfWork.GetRepository<Fase>().Create(entity);
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
