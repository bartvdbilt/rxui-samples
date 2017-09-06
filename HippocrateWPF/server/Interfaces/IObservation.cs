using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dbo;

namespace Interfaces
{
    public interface IObservation
    {
        List<Observation> CreateListObservation();
    }
}
