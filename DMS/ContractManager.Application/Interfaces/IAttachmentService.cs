using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContractManager.Domain.Entities;

namespace ContractManager.Application.Interfaces
{
    public interface IAttachmentService
    {
        Task<Attachment> AddAttachmentAsync(
            int contractId,
            string fileName,
            Stream fileStream,
            CancellationToken cancellationToken = default);

        Task<Stream?> GetAttachmentStreamAsync(
            int attachmentId,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAttachmentAsync(
            int attachmentId,
            CancellationToken cancellationToken = default);
    }
}
