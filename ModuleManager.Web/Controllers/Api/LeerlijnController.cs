using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ModuleManager.Domain;
using ModuleManager.Domain.Interfaces;
using ModuleManager.Web.Controllers.Api.Interfaces;

namespace ModuleManager.Web.Controllers.Api
{
    public class LeerlijnController : ApiController, IGenericApiController<Leerlijn>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LeerlijnController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet, Route("api/Leerlijn/Get")]
        public IEnumerable<Leerlijn> GetAll()
        {
            var maxSchooljaar = _unitOfWork.GetRepository<Schooljaar>().GetAll().Max(src => src.JaarId);
            var leerlijnen = _unitOfWork.GetRepository<Leerlijn>().GetAll().Where(src => src.Schooljaar.Equals(maxSchooljaar)).ToArray();
            return leerlijnen;
        }

        [HttpGet, Route("api/Leerlijn/Get/{schooljaar}/{key}")]
        public Leerlijn GetOne(string schooljaar, string key)
        {
            var leerlijn = _unitOfWork.GetRepository<Leerlijn>().GetOne(new object[] { key, schooljaar });
            return leerlijn;
        }

        [HttpPost, Route("api/Leerlijn/Delete")]
        public void Delete(Leerlijn entity)
        {
            _unitOfWork.GetRepository<Leerlijn>().Delete(entity);
        }

        [HttpPost, Route("api/Leerlijn/Edit")]
        public void Edit(Leerlijn entity)
        {
            _unitOfWork.GetRepository<Leerlijn>().Edit(entity);
        }

        [HttpPost, Route("api/Leerlijn/Create")]
        public void Create(Leerlijn entity)
        {
            _unitOfWork.GetRepository<Leerlijn>().Create(entity);
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
