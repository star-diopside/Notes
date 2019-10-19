using Microsoft.EntityFrameworkCore;
using Notes.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Notes.Data.Repositories
{
    public class UploadFileRepository : IUploadFileRepository
    {
        private readonly NotesDbContext _notesDbContext;

        public UploadFileRepository(NotesDbContext notesDbContext)
        {
            _notesDbContext = notesDbContext;
        }

        public IAsyncEnumerable<TResult> FindAllAsync<TResult>(Expression<Func<UploadFile, TResult>> selector)
        {
            return _notesDbContext.UploadFiles.OrderBy(u => u.Id)
                                              .Select(selector)
                                              .AsAsyncEnumerable();
        }

        public ValueTask<UploadFile> FindByIdAsync(int id)
        {
            return _notesDbContext.UploadFiles.FindAsync(id);
        }

        public async ValueTask<TResult> FindByIdAsync<TResult>(int id, Expression<Func<UploadFile, TResult>> selector)
        {
            return await _notesDbContext.UploadFiles.Where(u => u.Id == id)
                                                    .Select(selector)
                                                    .SingleOrDefaultAsync();
        }

        public async ValueTask<int> AddAsync(UploadFile uploadFile)
        {
            _notesDbContext.UploadFiles.Add(uploadFile);
            return await _notesDbContext.SaveChangesAsync();
        }

        public async ValueTask<int> UpdateAsync(UploadFile uploadFile)
        {
            _notesDbContext.Entry(uploadFile).Property(e => e.Version).OriginalValue = uploadFile.Version;
            _notesDbContext.UploadFiles.Update(uploadFile);
            return await _notesDbContext.SaveChangesAsync();
        }

        public async ValueTask<int> RemoveAsync(UploadFile uploadFile)
        {
            _notesDbContext.Entry(uploadFile).Property(e => e.Version).OriginalValue = uploadFile.Version;
            _notesDbContext.UploadFiles.Remove(uploadFile);
            return await _notesDbContext.SaveChangesAsync();
        }
    }
}
