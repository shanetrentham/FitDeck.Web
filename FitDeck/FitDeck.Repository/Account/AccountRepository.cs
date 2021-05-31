using Dapper;
using FitDeck.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FitDeck.Repository.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IConfiguration _config;
        public AccountRepository(IConfiguration config)
        {
            _config = config;
        }
        public async Task<IdentityResult> CreatAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var dataTable = new DataTable();

            dataTable.Columns.Add("UserName", typeof(string));
            dataTable.Columns.Add("NormalizedUserName", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));
            dataTable.Columns.Add("NormalizedEmail", typeof(string));
            dataTable.Columns.Add("FulName", typeof(string));
            dataTable.Columns.Add("Age", typeof(int));
            dataTable.Columns.Add("HeightInches", typeof(int));
            dataTable.Columns.Add("Weight", typeof(float));
            dataTable.Columns.Add("PasswordHash", typeof(string));

            dataTable.Rows.Add(
                user.UserName,
                user.NormalizedUserName,
                user.Email,
                user.NormalizedEmail,
                user.FullName,
                user.Age,
                user.HeightInches,
                user.Weight,
                user.PasswordHash
                );
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync("AddUser",
                    new { Account = dataTable.AsTableValuedParameter("dbo.AccountType") },
                    commandType: CommandType.StoredProcedure);
            }
            return IdentityResult.Success;
        }

        public async Task<ApplicationUserIdentity> GetByUserNameAsync(string username, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ApplicationUserIdentity user;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                user = await connection.QuerySingleOrDefaultAsync<ApplicationUserIdentity>("GetUserByUserName",
                    new { UserName = username },
                    commandType: CommandType.StoredProcedure);
            }
            return user;
        }
    }
}
