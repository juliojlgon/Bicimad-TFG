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
    public class SecurityQueryService : ISecurityQueryService
    {
        private readonly IRepository _repository;
        private const int ExpirationMinutes = 10080;

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
                if (parts.Length == 4)
                {
                    // Get the hash message, username, and timestamp.
                    var hash = parts[0];
                    var userId = parts[1];
                    var username = parts[2];
                    var ticks = long.Parse(parts[3]);
                    var timeStamp = new DateTime(ticks);

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
                // ignored
            }

            return result;
        }
    }
}
