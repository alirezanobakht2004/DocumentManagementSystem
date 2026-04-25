using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractManager.Application.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveAttachmentAsync(
            int? contractId,
            string originalFileName,
            Stream fileStream,
            CancellationToken cancellationToken = default);

        Task<Stream> OpenReadAsync(
            string filePath,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(
            string filePath,
            CancellationToken cancellationToken = default);

        bool FileExists(string filePath);
    }
}
