using DP.Application.Dtos.Auth;
using DP.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.EntityDtoMapping;
public static class UserMapper
{
    public static UserResponse ToDto(this AppUser entity)
    {
        return new UserResponse(entity.Id, string.Empty);
    }

    public static AppUser ToEntity(this UserResponse dto)
    {
        return new AppUser
        {
            Id = dto.UserId,
        };
    }
}
