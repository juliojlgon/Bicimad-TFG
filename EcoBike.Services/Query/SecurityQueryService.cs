using System;
using System.Linq;
using System.Text;
using Bicimad.Core;
using Bicimad.Services.Query.Interfaces;

namespace Bicimad.Services.Query
{
    public class SecurityQueryService : ISecurityQueryService
    {
        private const int ExpirationMinutes = 10080;
        private readonly IRepository _repository;

        public SecurityQueryService(IRepository repostory)
        {
            _repository = repostory;
        }

        public bool IsTokenValid(string token)
        {
            var result = false;
            try
            {
                // Base64 decode the string, obtaining the token:username:timeStamp.
                var key = Encoding.UTF8.GetString(Convert.FromBase64String(token));

                // Split the parts.
                var parts = key.Split(':');
                if (parts.Length == 5)
                {
                    // Get the hash message, username, and timestamp.
                    var hash = parts[0];
                    var userId = parts[1];
                    var username = parts[2];
                    var ticks = long.Parse(parts[3]);
                    var timeStamp = new DateTime(ticks);
                    var isAdmin = parts[4];

                    // Ensure the timestamp is valid.
                    var expired = Math.Abs((DateTime.UtcNow - timeStamp).TotalMinutes) > ExpirationMinutes;
                    if (!expired)
                    {
                        var user = _repository.Users.FirstOrDefault(u => u.UserName == username);
                        //
                        // Lookup the user's account from the db.
                        //
                        result = (user != null);
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return result;
        }
    }
}