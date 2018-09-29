using Notes.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.Web.Services
{
    public interface IUploadFileService
    {
        Task<IEnumerable<UploadFile>> SelectAllAsync();

        Task CreateAsync(UploadFile uploadFile);
    }
}
