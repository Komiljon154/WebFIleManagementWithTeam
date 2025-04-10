namespace WebFIleManagementt.StorageBroker.Services;

public interface IStorageBrokerService
{
    Task CreateDirectoryAsync(string directoryPath);
    Task UploadFileAsync(string? directoryPath, Stream stream);
    Task<List<string>> GetAllFilesAndDirectoriesAsync(string? directoryPath);
    Task<Stream> DownloadFileAsync(string filePath);
    Task<Stream> DownloadFolderAsZipAsync(string directoryPath);
    Task DeleteFileAsync(string filePath);
    Task DeleteDirectoryAsync(string directoryPath);
}
