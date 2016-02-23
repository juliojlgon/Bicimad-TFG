namespace Bicimad.Services.Query.Dto.User
{
    public class UserLoginDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FriendlyUrlUserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }

        public bool IsAdmin { get; set; }

        public string Avatar { get; set; }
    }
}