using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ModuleManager.Domain;
using ModuleManager.Web.Controllers.Api.Interfaces;
using ModuleManager.Domain.Interfaces;

namespace ModuleManager.Web.Controllers.Api
{
    public class TagController : ApiController, IGenericApiController<Tag>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet, Route("api/Tag/Get")]
        public IEnumerable<Tag> GetAll()
        {
            var maxSchooljaar = _unitOfWork.GetRepository<Schooljaar>().GetAll().Max(src => src.JaarId);
            var tags = _unitOfWork.GetRepository<Tag>().GetAll().ToArray();
            return tags;
        }

        [HttpGet, Route("api/Tag/Get/{key}")]
        public Tag GetOne(int schooljaar, string key)
        {
            var tag = _unitOfWork.GetRepository<Tag>().GetOne(new object[] { key });
            return tag;
        }

        [HttpPost, Route("api/Tag/Delete")]
        public void Delete(Tag entity)
        {
            _unitOfWork.GetRepository<Tag>().Delete(entity);
        }

        [HttpPost, Route("api/Tag/Edit")]
        public void Edit(Tag entity)
        {
            _unitOfWork.GetRepository<Tag>().Edit(entity);
        }

        [HttpPost, Route("api/Tag/Create")]
        public void Create(Tag entity)
        {
            _unitOfWork.GetRepository<Tag>().Create(entity);
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
