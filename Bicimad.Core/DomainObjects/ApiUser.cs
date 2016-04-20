using System;
using Bicimad.Core.DomainObjects.Interfaces;

namespace Bicimad.Core.DomainObjects
{
    public class ApiUser : IEntity
    {
        public string Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Token { get; set; }

        public string ApiKey { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
