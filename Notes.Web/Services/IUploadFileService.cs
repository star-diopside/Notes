using Notes.Data.Models;
using Notes.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.Web.Services
{
    public interface IUploadFileService
    {
        ValueTask<IEnumerable<UploadFileViewModel>> ListAsync();

        ValueTask<UploadFileViewModel> GetDetailsAsync(int id);

        ValueTask<UploadFile> GetDownloadDataAsync(int id);

        ValueTask CreateAsync(UploadFileViewModel uploadFile);

        ValueTask EditAsync(int id, UploadFileViewModel uploadFile);

        ValueTask DeleteAsync(int id, uint version);
    }
}
