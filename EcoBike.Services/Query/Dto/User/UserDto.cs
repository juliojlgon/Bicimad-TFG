using System;

namespace Bicimad.Services.Query.Dto.User
{
    public class UserDto
    {
        public string Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UserName { get; set; }

        public string FriendlyUrlUserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }

        public bool IsAdmin { get; set; }

        public string Avatar { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime? BornDate { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }
    }
}
