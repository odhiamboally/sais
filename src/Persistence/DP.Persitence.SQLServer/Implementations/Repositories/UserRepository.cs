using DP.Domain.Entities;
using DP.Domain.IRepositories;
using DP.Persistence.SQLServer.DataContext;
using DP.Persistence.SQLServer.Implementations.Repositories;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DP.Infrastructure.Implementations.Repositories;

internal sealed class UserRepository : BaseRepository<AppUser>, IUserRepository
{

    public UserRepository(DBContext context) : base(context)
    {
    }
    

    
}
