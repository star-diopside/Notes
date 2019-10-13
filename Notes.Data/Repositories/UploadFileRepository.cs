﻿using Microsoft.EntityFrameworkCore;
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
            return await _notesDbContext.UploadFiles.OrderBy(u => u.Id)
                                                    .Select(selector)
                                                    .ToListAsync();
        }

        public ValueTask<UploadFile> FindByIdAsync(int id)
        {
            return _notesDbContext.UploadFiles.FindAsync(id);
        }

        public Task<TResult> FindByIdAsync<TResult>(int id, Expression<Func<UploadFile, TResult>> selector)
        {
            return _notesDbContext.UploadFiles.Where(u => u.Id == id)
                                              .Select(selector)
                                              .SingleOrDefaultAsync();
        }

        public Task<int> AddAsync(UploadFile uploadFile)
        {
            _notesDbContext.UploadFiles.Add(uploadFile);
            return _notesDbContext.SaveChangesAsync();
        }

        public Task<int> UpdateAsync(UploadFile uploadFile)
        {
            _notesDbContext.Entry(uploadFile).Property(e => e.Version).OriginalValue = uploadFile.Version;
            _notesDbContext.UploadFiles.Update(uploadFile);
            return _notesDbContext.SaveChangesAsync();
        }

        public Task<int> RemoveAsync(UploadFile uploadFile)
        {
            _notesDbContext.Entry(uploadFile).Property(e => e.Version).OriginalValue = uploadFile.Version;
            _notesDbContext.UploadFiles.Remove(uploadFile);
            return _notesDbContext.SaveChangesAsync();
        }
    }
}
