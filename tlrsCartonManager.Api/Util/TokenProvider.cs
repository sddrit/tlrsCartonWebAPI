using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using tlrsCartonManager.DAL.Models;

namespace tlrsCartonManager.Api.Util
{
    public class TokenProvider : DataProtectorTokenProvider<User>
    {
        public TokenProvider(IDataProtectionProvider dataProtectionProvider, IOptions<DataProtectionTokenProviderOptions> options,
            ILogger<DataProtectorTokenProvider<User>> logger)
            : base(dataProtectionProvider, options, logger)
        {
            this.Options.TokenLifespan = TimeSpan.FromDays(1);
        }

        public override Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<User> manager, User user)
        {
            return base.CanGenerateTwoFactorTokenAsync(manager, user);
        }
    }
}
