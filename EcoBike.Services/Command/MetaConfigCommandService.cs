using System.Linq;
using Bicimad.Core;
using Bicimad.Core.DomainObjects;
using Bicimad.Helpers;
using Bicimad.Services.Command.Commands;
using Bicimad.Services.Command.Commands.MetaConfig;
using Bicimad.Services.Command.Interface;

namespace Bicimad.Services.Command
{
    public class MetaConfigCommandService : BaseCommandService, IMetaConfigCommandService
    {
        public MetaConfigCommandService(IRepository repository) : base(repository)
        {
        }

        public CommandResult Create(CreateMetaConfigCommand command)
        {
            var commandResult = Validate(command);
            if (!commandResult.Success) return commandResult;


            if (command.DeleteExistentKeys == true)
            {
                var remove = Repository.MetaConfigs.Where(m => m.MetaKey == command.MetaKey);
                Repository.MetaConfigs.RemoveRange(remove);
                Repository.Commit();
            }

            Repository.MetaConfigs.Add(new MetaConfig
            {
                Id = GuidHelper.GenerateId(),
                CreatedDate = DateTimeHelper.SpanishNow,
                MetaKey = command.MetaKey,
                MetaValue = command.MetaValue
            });

            Repository.Commit();

            return commandResult;
        }
    }
}