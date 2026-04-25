using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContractManager.Application.Interfaces;
using ContractManager.Infrastructure.Data;

namespace ContractManager.Infrastructure.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly AppDbContext _db;
        private readonly IFileStorageService _fileStorage;

        public AttachmentService(
            AppDbContext db,
            IFileStorageService fileStorage)
        {
            _db = db;
            _fileStorage = fileStorage;
        }

        public async Task<Attachment> AddAttachmentAsync(
            int contractId,
            string fileName,
            Stream fileStream,
            CancellationToken cancellationToken = default)
        {
            // 1. ذخیره فایل روی دیسک
            var relativePath = await _fileStorage.SaveAttachmentAsync(
                contractId,
                fileName,
                fileStream,
                cancellationToken);

            // 2. ساخت رکورد Attachment
            var attachment = new Attachment
            {
                ContractId = contractId,
                RelatedEntityType = EntityType.Contract,
                RelatedEntityId = contractId,
                FilePath = relativePath,
                FileType = Path.GetExtension(fileName).Trim('.'),
                FileSize = fileStream.Length,
                HasOCR = false
            };

            _db.Attachments.Add(attachment);
            await _db.SaveChangesAsync(cancellationToken);

            return attachment;
        }

        public async Task<Stream?> GetAttachmentStreamAsync(
            int attachmentId,
            CancellationToken cancellationToken = default)
        {
            var attachment = await _db.Attachments
                .FirstOrDefaultAsync(a => a.Id == attachmentId, cancellationToken);

            if (attachment == null)
                return null;

            if (!_fileStorage.FileExists(attachment.FilePath))
                return null;

            return await _fileStorage.OpenReadAsync(attachment.FilePath, cancellationToken);
        }

        public async Task<bool> DeleteAttachmentAsync(
            int attachmentId,
            CancellationToken cancellationToken = default)
        {
            var attachment = await _db.Attachments
                .FirstOrDefaultAsync(a => a.Id == attachmentId, cancellationToken);

            if (attachment == null)
                return false;

            await _fileStorage.DeleteAsync(attachment.FilePath, cancellationToken);

            _db.Attachments.Remove(attachment);
            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
