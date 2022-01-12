using System.IO;
using  Microsoft.AspNetCore.Components.Forms;
using Infrastructure.Common;

namespace Infrastructure.Services
{
    public class FileManager
    {
        // to do :
        // check if exist or add GUID to file name

        public string GetDataStoragePath(string subfolder)
        {
            string directory = ".datastorage";
            string storage = Directory.GetCurrentDirectory();
            storage = Path.GetFullPath(Path.Combine(storage, @"..\..\..\"));

            return Path.GetFullPath(Path.Combine(storage, directory, subfolder));
        }

        public string GetFilePath(IBrowserFile file, string subfolder)
        {
            string directoryPath = GetDataStoragePath(subfolder);
            return Path.GetFullPath(Path.Combine(directoryPath, file.Name));
        }

        // private string Rename(string subfolder)
        // {
        //     string guid = Guid.NewGuid().ShortenGuid();

        // }

        public async Task UploadAsync(IBrowserFile file, string subfolder)
        {
            string filePath = GetFilePath(file, subfolder);
            Stream stream = file.OpenReadStream();
            FileStream fileStream = File.Create(filePath);

            await stream.CopyToAsync(fileStream);

            stream.Close();
            fileStream.Close();

        }

        public async Task UploadAsync(IBrowserFile file, string subfolder, string rename)
        {
            string filePath = GetFilePath(file, subfolder);
            Stream stream = file.OpenReadStream();
            FileStream fileStream = File.Create(filePath);

            await stream.CopyToAsync(fileStream);

            stream.Close();
            fileStream.Close();

            string directory = GetDataStoragePath(subfolder);
            string renamePath = Path.GetFullPath(Path.Combine(directory, rename));
            File.Move(filePath, renamePath);

        }

        public string RenameFile(IBrowserFile file, string subfolder)
        {
            string filePath = GetFilePath(file, subfolder);
            string directory = GetDataStoragePath(subfolder);
            string guid = Guid.NewGuid().ShortenGuid();
            string extension = Path.GetExtension(filePath);
            string rename = Path.GetFullPath(Path.Combine(directory, $"{guid}{extension}"));

            File.Move(filePath, rename);

            return rename;
        }

        public void Upload(IBrowserFile file, string subfolder)
        {
            string filePath = GetFilePath(file, subfolder);
            Stream stream = file.OpenReadStream();
            FileStream fileStream = File.Create(filePath);
            // System.Console.WriteLine($"PATH: {filePath}");
            stream.CopyTo(fileStream);

            stream.Close();
            fileStream.Close();
        }
    }
}