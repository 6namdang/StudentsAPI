﻿using Microsoft.AspNetCore.Identity;

namespace Students.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> role);
    }
}
