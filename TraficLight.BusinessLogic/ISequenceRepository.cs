using System;
using System.Threading.Tasks;

namespace TraficLight.BusinessLogic
{
    public interface ISequenceRepository : IDisposable
    {
        Task<Answer> Create();
        Task<Answer> Add(Request request);
        Task<Answer> Clear();
    }
}
