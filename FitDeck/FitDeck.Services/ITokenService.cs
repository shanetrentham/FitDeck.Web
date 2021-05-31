using FitDeck.Models.Account;
using System;

namespace FitDeck.Services
{
    public interface ITokenService
    {
        public string CreateToken(ApplicationUserIdentity user);
    }
}
