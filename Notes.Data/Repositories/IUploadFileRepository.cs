using Notes.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Notes.Data.Repositories
{
    public interface IUploadFileRepository
    {
        Task<IEnumerable<TResult>> FindAllAsync<TResult>(Expression<Func<UploadFile, TResult>> selector);

        Task<UploadFile> FindByIdAsync(int id);

        Task<TResult> FindByIdAsync<TResult>(int id, Expression<Func<UploadFile, TResult>> selector);

        Task AddAsync(UploadFile uploadFile);

        Task UpdateAsync(UploadFile uploadFile);

        Task RemoveAsync(UploadFile uploadFile);
    }
}
