
using DP.Domain.Interfaces;
using DP.Domain.IRepositories;
using DP.Persistence.SQLServer.DataContext;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Persistence.SQLServer.Implementations.Intefaces;
public class UnitOfWork : IUnitOfWork
{
    public IUserRepository UserRepository { get; private set; }
    public IApplicantRepository ApplicantRepository { get; private set; }
    public IApplicationRepository ApplicationRepository { get; private set; }
    

    private readonly DBContext _context;

    public UnitOfWork(
        IUserRepository userRepository,
        IApplicantRepository applicantRepository,
        IApplicationRepository applicationRepository,

        

        DBContext Context


        )
    {

        UserRepository = userRepository;
        ApplicantRepository = applicantRepository;
        ApplicationRepository = applicationRepository;

        _context = Context;
        

    }

    public async Task<int> CompleteAsync()
    {
        var result = await _context.SaveChangesAsync();
        return result!;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);

    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();
        }
    }
}
