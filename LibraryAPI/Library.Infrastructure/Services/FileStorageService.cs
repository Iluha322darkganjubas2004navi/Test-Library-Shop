using Library.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Services
{
    public class FileStorageService : IFileStorage
    {
        private const string RootLocation = "uploads";

        public async Task<Stream> GetFileStreamAsync(string relativePath)
        {
            var filePath = Path.Combine(RootLocation, relativePath);
            if (!File.Exists(filePath))
                filePath = Path.Combine(RootLocation, "defaultIcon.png");

            return await Task.FromResult(File.OpenRead(filePath));
        }

        public async Task<bool> SaveFileAsync(string relativePath, Stream fileStream, CancellationToken cancellationToken)
        {
            var filePath = Path.Combine(RootLocation, relativePath);
            var directory = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (File.Exists(filePath))
                File.Delete(filePath);

            await using var file = File.Create(filePath);
            await fileStream.CopyToAsync(file, cancellationToken);
            return true;
        }
    }
}
