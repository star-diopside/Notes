using Notes.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.Data.Repositories
{
    public interface IUploadFileRepository
    {
        Task<IEnumerable<UploadFile>> ListAllAsync();

        Task AddAsync(UploadFile uploadFile);
    }
}
