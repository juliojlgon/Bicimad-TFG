using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bicimad.Core.DomainObjects.Interfaces
{
    public interface IEntity
    {
        string Id { get; set; }

        DateTime CreatedDate { get; set; } 
    }
}
