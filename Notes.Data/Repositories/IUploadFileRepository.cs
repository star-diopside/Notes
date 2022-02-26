using Notes.Data.Models;
using System.Linq.Expressions;

namespace Notes.Data.Repositories;

public interface IUploadFileRepository
{
    IAsyncEnumerable<TResult> FindAllAsync<TResult>(Expression<Func<UploadFile, TResult>> selector);

    ValueTask<UploadFile> FindByIdAsync(int id);

    ValueTask<TResult> FindByIdAsync<TResult>(int id, Expression<Func<UploadFile, TResult>> selector);

    ValueTask<int> AddAsync(UploadFile uploadFile);

    ValueTask<int> UpdateAsync(UploadFile uploadFile);

    ValueTask<int> RemoveAsync(UploadFile uploadFile);
}
