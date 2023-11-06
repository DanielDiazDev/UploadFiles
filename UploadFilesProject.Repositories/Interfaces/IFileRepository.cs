using UploadFilesProject.Models;

namespace UploadFilesProject.Repositories.Interfaces
{
    public interface IFileRepository
    {
        Task AddFile(int id, string fileName, byte[] FileData, string userId);
        Task<UserFile> GetFile(int id);
        IEnumerable<UserFile> GetFiles(string id);
        Task UpdateFile(UserFile oUserFile);
        Task DeleteFile(int id);
    }
}
