using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Try3.Models;

namespace Try3.Entities
{
    public interface IPlaceRepository
    {
        IEnumerable<place> Places { get; }
    }
}