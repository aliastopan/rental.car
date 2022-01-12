using System.IO;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Infrastructure.Common;

namespace Infrastructure.Services
{
    public class FileManager
    {
        private IWebHostEnvironment? env { get; init; }

        public FileManager(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public string GetRootDirectory(string subfolder)
        {
            return $"{env!.WebRootPath}\\{subfolder}";
        }

        public string GetDataStoragePath(string subfolder)
        {
            string directory = ".datastorage";
            string storage = Directory.GetCurrentDirectory();
            storage = Path.GetFullPath(Path.Combine(storage, @"..\..\..\"));

            return Path.GetFullPath(Path.Combine(storage, directory, subfolder));
        }

        public string GetFilePath(IBrowserFile file, string subfolder)
        {
            string directoryPath = GetRootDirectory(subfolder);
            return Path.GetFullPath(Path.Combine(directoryPath, file.Name));
        }

        public async Task UploadAsync(IBrowserFile file, string subfolder)
        {
            string filePath = GetFilePath(file, subfolder);
            Stream stream = file.OpenReadStream();
            FileStream fileStream = File.Create(filePath);

            await stream.CopyToAsync(fileStream);

            stream.Close();
            fileStream.Close();

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

        public string RenameFile(IBrowserFile file, string subfolder)
        {
            string filePath = GetFilePath(file, subfolder);
            string directory = GetRootDirectory(subfolder);
            string guid = Guid.NewGuid().ShortenGuid();
            string extension = Path.GetExtension(filePath);
            string rename = Path.GetFullPath(Path.Combine(directory, $"{guid}{extension}"));

            File.Move(filePath, rename);

            return rename;
        }
    }
}