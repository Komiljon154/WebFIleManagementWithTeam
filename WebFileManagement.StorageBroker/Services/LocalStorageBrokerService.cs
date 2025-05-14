using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WebFileManagement.StorageBroker.Services;

public class LocalStorageBrokerService : IStorageBrokerService
{
    private readonly string _dataPath;
    public LocalStorageBrokerService()
    {
        _dataPath = Path.Combine(Directory.GetCurrentDirectory(), "data");
        if (!Directory.Exists(_dataPath))
            Directory.CreateDirectory(_dataPath);
    }
    public void CreateDirectory(string directoryPath)
    {
        directoryPath = Path.Combine(_dataPath, directoryPath);
        ValidateDirectoryPath(directoryPath);
        Directory.CreateDirectory(directoryPath);
    }

    public List<string> GetAllFilesAndDirectories(string directoryPath)
    {
        directoryPath = Path.Combine(_dataPath, directoryPath);

        DirectoryInfo? parentPath = Directory.GetParent(directoryPath);
        if (!Directory.Exists(parentPath.FullName))
        {
            throw new Exception("Parent folder not found");
        }

        var allFIlesAndFolders = Directory.GetFileSystemEntries(directoryPath).ToList();
        //allFIlesAndFolders = allFIlesAndFolders.Select(nm => nm.Substring(nm.LastIndexOf("\\") + 1)).ToList(); // It also works
        allFIlesAndFolders = allFIlesAndFolders.Select(name => name.Remove(0, directoryPath.Length + 1)).ToList();
        return allFIlesAndFolders;
    }

    public void UploadFile(string filePath, Stream stream)
    {
        filePath = Path.Combine(_dataPath, filePath);
        var parentPath = Directory.GetParent(filePath);
        if (!Directory.Exists(parentPath.FullName)) 
        {
            throw new Exception("Parent folder not found");
        }
        using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            stream.CopyTo(fileStream);
        }
    }
    private void ValidateDirectoryPath(string directoryPath)
    {
        if (Directory.Exists(directoryPath))
        {
            throw new Exception("Folder has already created");
        }
        DirectoryInfo? parentPath = Directory.GetParent(directoryPath);

        if (!Directory.Exists(parentPath.FullName))
        {
            throw new Exception("Parent folder not found");
        }
    }
}
