using System;
using System.Collections.Generic;
using System.Text;

namespace TraficLight.BusinessLogic
{
    public interface ISequenceRepository : IDisposable
    {
        Answer Create();
        Answer Add(Request request);
        Answer Clear();
    }
}
