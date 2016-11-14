using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ModuleManager.Domain;
using ModuleManager.Domain.Interfaces;
using ModuleManager.Web.Controllers.Api.Interfaces;

namespace ModuleManager.Web.Controllers.Api
{
    public class CompetentieController : ApiController, IGenericApiController<Competentie>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompetentieController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet, Route("api/Competentie/Get")]
        public IEnumerable<Competentie> GetAll()
        {
            var maxSchooljaar = _unitOfWork.GetRepository<Schooljaar>().GetAll().Max(src => src.JaarId);
            var competenties = _unitOfWork.GetRepository<Competentie>().GetAll().ToArray();
            return competenties;
        }

        [HttpGet, Route("api/Competentie/Get/{schooljaar}/{key}")]
        public Competentie GetOne(string schooljaar, string key)
        {
            var competentie = _unitOfWork.GetRepository<Competentie>().GetOne(new object[] { key, schooljaar });
            return competentie;
        }

        [HttpPost, Route("api/Competentie/Delete")]
        public void Delete(Competentie entity)
        {
            _unitOfWork.GetRepository<Competentie>().Delete(entity);
        }

        [HttpPost, Route("api/Competentie/Edit")]
        public void Edit(Competentie entity)
        {
            _unitOfWork.GetRepository<Competentie>().Edit(entity);
        }

        [HttpPost, Route("api/Competentie/Create")]
        public void Create(Competentie entity)
        {
            _unitOfWork.GetRepository<Competentie>().Create(entity);
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
