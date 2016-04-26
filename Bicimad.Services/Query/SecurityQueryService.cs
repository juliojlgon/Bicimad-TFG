using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bicimad.Core;
using Bicimad.Helpers;
using Bicimad.Services.Query.Interfaces;

namespace Bicimad.Services.Query
{
    public class SecurityQueryService: ISecurityQueryService
    {
        private readonly IRepository _repository;
        private const int ExpirationMinutes = 10080;

        public SecurityQueryService( IRepository repostory)
        {
            _repository = repostory;
        }

        public SecurityQueryService(EFRepository repository)
        {
            throw new NotImplementedException();
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
                if (parts.Length == 3)
                {
                    // Get the hash message, username, and timestamp.
                    var hash = parts[0];
                    var username = parts[1];
                    var ticks = long.Parse(parts[2]);
                    var timeStamp = new DateTime(ticks);

                    // Ensure the timestamp is valid.
                    var expired = Math.Abs((DateTime.UtcNow - timeStamp).TotalMinutes) > ExpirationMinutes;
                    if (!expired)
                    {
                        var user = _repository.Users.FirstOrDefault(u => u.UserName == username);
                        //
                        // Lookup the user's account from the db.
                        //
                        if (user != null)
                        {
                            var password = user.Password;

                            // Hash the message with the key to generate a token.
                            var computedToken = HashHelper.GenerateToken(username, password);

                            // Compare the computed token with the one supplied and ensure they match.
                            result = (token == computedToken);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return result;
        }
    }
}
