using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FitDeck.Models.Account;


namespace FitDeck.Repository.Account
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> CreatAsync(ApplicationUserIdentity user, CancellationToken cancellationToken);

        public Task<ApplicationUserIdentity> GetByUserNameAsync(string username, CancellationToken cancellationToken);
    }
}
