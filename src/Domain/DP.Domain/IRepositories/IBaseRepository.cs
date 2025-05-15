using DP.Domain.ValueObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DP.Domain.IRepositories;
public interface IBaseRepository<T> where T : class
{
    Task<T> CreateAsync(T entity);
    Task<T> DeleteAsync(int Id);
    IQueryable<T> FindAll();
    Task<T?> FindByIdAsync(int Id);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    IQueryable<T> FindFiltered(List<IFilterCriteria> filters);
    Task<T> UpdateAsync(T entity);
    
}
