
using DP.Domain.IRepositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Domain.Interfaces;
public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IApplicantRepository ApplicantRepository { get; }
    IApplicationRepository ApplicationRepository { get; }


    Task<int> CompleteAsync();
}
