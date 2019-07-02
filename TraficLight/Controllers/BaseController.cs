using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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

        [HttpPost, Route("sequence/create")]
        public async Task<Answer> Create()
        {
            return await _repo.Create();
        }

        [HttpPost, Route("observation/add")]
        public async Task<Answer> Add([FromBody]Request request)
        {
            return await _repo.Add(request);
        }

        [HttpGet, Route("clear")]
        public async Task<Answer> Clear()
        {
            return await _repo.Clear();
        }
    }
}
