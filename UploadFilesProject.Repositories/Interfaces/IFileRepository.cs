using UploadFilesProject.Models;

namespace UploadFilesProject.Repositories.Interfaces
{
    public interface IFileRepository
    {
        Task AddFile(int id, string filename, string contentTipe, byte[] data, string userId);
        Task<UserFile> GetFile(int id);
        IEnumerable<UserFile> GetFiles();
        Task UpdateFile(UserFile oUserFile);
        Task DeleteFile(int id);
    }
}
