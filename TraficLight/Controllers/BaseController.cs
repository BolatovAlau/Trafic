using Microsoft.AspNetCore.Mvc;
using TraficLight.BusinessLogic;

namespace TraficLight.Controllers
{
    [Route(""), ApiController]
    public class BaseController : ControllerBase
    {
        private ISequenceRepository _repo;

        public BaseController(ISequenceRepository repo)
        {
            _repo = repo;
        }

        [HttpPost, Route("/sequence/create")]
        public ActionResult<Answer> Create()
        {
            return _repo.Create();
        }

        [HttpPost, Route("/observation/add")]
        public ActionResult<Answer> Add([FromBody]Request request)
        {
            return _repo.Add(request);
        }

        [HttpGet, Route("clear")]
        public ActionResult<Answer> Clear()
        {
            return _repo.Clear();
        }
    }
}
