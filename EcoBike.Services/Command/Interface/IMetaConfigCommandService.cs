using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bicimad.Services.Command.Commands;
using Bicimad.Services.Command.Commands.MetaConfig;

namespace Bicimad.Services.Command.Interface
{
    public interface IMetaConfigCommandService
    {
        CommandResult Create(CreateMetaConfigCommand command);
    }
}
