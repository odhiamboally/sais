using DP.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Contracts.Abstractions.IServices;
public interface IClaimsService
{
    Task<List<Claim>> GetUserClaimsAsync(AppUser appUser);
}
