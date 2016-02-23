namespace Bicimad.Web.Models.AccountModels
{
    public class UserLoggedModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string FriendlyUrlName { get; set; }
        public bool IsAdmin { get; set; }

        public string SerializeForCookie()
        {
            var aux = string.Format("{0};{1};{2};{3};{4};{5}", Id, Email, Name, Avatar, FriendlyUrlName, IsAdmin);
            return aux;
        }
    }
}