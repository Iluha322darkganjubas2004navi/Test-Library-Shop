using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Services.Interfaces
{
    public interface IFileStorage
    {
        Task<Stream> GetFileStreamAsync(string relativePath);
        Task<bool> SaveFileAsync(string relativePath, Stream fileStream, CancellationToken cancellationToken);
    }
}
