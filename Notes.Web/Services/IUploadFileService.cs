using Notes.Data.Models;
using Notes.Web.Models;

namespace Notes.Web.Services;

public interface IUploadFileService
{
    IAsyncEnumerable<UploadFileViewModel> ListAsync();

    ValueTask<UploadFileViewModel?> GetDetailsAsync(int id);

    ValueTask<UploadFile?> GetDownloadDataAsync(int id);

    ValueTask<UploadFile> CreateAsync(UploadFileViewModel uploadFile);

    ValueTask<UploadFile?> EditAsync(int id, UploadFileViewModel uploadFile);

    ValueTask DeleteAsync(int id, uint version);
}
