using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bicimad.Core;
using Bicimad.Core.DomainObjects;
using Bicimad.Helpers;
using Bicimad.Services.Command.Commands;
using Bicimad.Services.Command.Interface;

namespace Bicimad.Services.Command
{
    public class StationCommandService: BaseCommandService, IStationCommandService
    {
        public StationCommandService(IRepository repository) : base(repository)
        {
        }

        
    }
}
