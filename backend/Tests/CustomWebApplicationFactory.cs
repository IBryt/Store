using IgorBryt.Store.DAL.Data;
using IgorBryt.Store.Tests;
using IgorBryt.Store.WebAPI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Library.Tests.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
{

    private static readonly List<IdentityUser> _users = new List<IdentityUser>();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Standing");

        builder.ConfigureServices(services =>
        {
            RemoveLibraries(services);

            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("InMemoryDatabase"));

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseInMemoryDatabase("InMemoryDatabaseIdentity"));

            services.AddTransient<IUserStore<IdentityUser>, InMemoryUserStore>();
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                _users.Clear();
                UnitTestHelper.SeedData(context);
                UnitTestHelper.SeedIdentityData(scope);
            }
        });
    }

    private static void RemoveLibraries(IServiceCollection services)
    {
        var descriptorsToRemove = services.Where(descriptor =>
           descriptor.ServiceType == typeof(DbContextOptions<AppDbContext>)
           ).ToList();

        foreach (var descriptor in descriptorsToRemove)
        {
            services.Remove(descriptor);
        }
    }

    private class InMemoryUserStore : IUserPasswordStore<IdentityUser>, IUserEmailStore<IdentityUser>
    {

        public Task<IdentityResult> CreateAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            _users.Add(user);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> UpdateAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var user = _users.Find(u => u.Id == userId);
            return Task.FromResult(user);
        }

        public Task<IdentityUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var user = _users.Find(u => u.NormalizedUserName == normalizedUserName);
            return Task.FromResult(user);
        }

        public Task<string> GetUserIdAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetUserNameAsync(IdentityUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task SetNormalizedUserNameAsync(IdentityUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
        }

        public Task SetEmailAsync(IdentityUser user, string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            var user = _users.Find(u => u.NormalizedEmail == normalizedEmail);
            return Task.FromResult(user);
        }

        public Task<string> GetNormalizedEmailAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedEmailAsync(IdentityUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.CompletedTask;
        }
    }
}
