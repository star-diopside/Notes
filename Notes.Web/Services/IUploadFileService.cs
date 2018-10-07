using Notes.Data.Models;
using Notes.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.Web.Services
{
    public interface IUploadFileService
    {
        Task<IEnumerable<UploadFileViewModel>> ListAsync();

        Task<UploadFileViewModel> GetDetailsAsync(int id);

        Task<UploadFile> GetDownloadDataAsync(int id);

        Task CreateAsync(UploadFileViewModel uploadFile);

        Task EditAsync(int id, UploadFileViewModel uploadFile);

        Task DeleteAsync(int id, uint version);
    }
}
