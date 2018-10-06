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

        public async Task<IEnumerable<TResult>> FindAllAsync<TResult>(Expression<Func<UploadFile, TResult>> selector)
        {
            return await _notesDbContext.UploadFiles.OrderBy(u => u.Id).Select(selector).ToListAsync();
        }

        public async Task<UploadFile> FindByIdAsync(int id)
        {
            return await _notesDbContext.UploadFiles.SingleAsync(u => u.Id == id);
        }

        public async Task<TResult> FindByIdAsync<TResult>(int id, Expression<Func<UploadFile, TResult>> selector)
        {
            return await _notesDbContext.UploadFiles.Where(u => u.Id == id).Select(selector).SingleAsync();
        }

        public async Task AddAsync(UploadFile uploadFile)
        {
            _notesDbContext.UploadFiles.Add(uploadFile);
            await _notesDbContext.SaveChangesAsync();
        }
    }
}
