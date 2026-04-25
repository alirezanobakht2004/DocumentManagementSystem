using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ContractManager.Application.Configuration;
using ContractManager.Application.Interfaces;

namespace ContractManager.Infrastructure.Storage
{
    public class FileStorageService : IFileStorageService
    {
        private readonly FileStorageOptions _options;

        public FileStorageService(FileStorageOptions options)
        {
            _options = options;

            // اطمینان از وجود Root Folder
            if (!Directory.Exists(_options.RootPath))
            {
                Directory.CreateDirectory(_options.RootPath);
            }
        }

        public async Task<string> SaveAttachmentAsync(
            int? contractId,
            string originalFileName,
            Stream fileStream,
            CancellationToken cancellationToken = default)
        {
            if (fileStream == null || !fileStream.CanRead)
                throw new ArgumentException("Invalid file stream", nameof(fileStream));

            var now = DateTime.UtcNow;

            var safeFileName = MakeSafeFileName(Path.GetFileNameWithoutExtension(originalFileName));
            var ext = Path.GetExtension(originalFileName);

            string baseFolder;

            if (contractId.HasValue && contractId.Value > 0)
            {
                baseFolder = Path.Combine(
                    _options.RootPath,
                    "Contracts",
                    contractId.Value.ToString(),
                    "Attachments",
                    now.Year.ToString("0000"),
                    now.Month.ToString("00"));
            }
            else
            {
                // اگر Attachment هنوز به Contract وصل نشده
                baseFolder = Path.Combine(
                    _options.RootPath,
                    "Orphans",
                    now.Year.ToString("0000"),
                    now.Month.ToString("00"));
            }

            if (!Directory.Exists(baseFolder))
                Directory.CreateDirectory(baseFolder);

            // اسم فایل نهایی: {ticks}_{safeName}{ext}
            var uniquePrefix = DateTime.UtcNow.Ticks.ToString();
            var finalFileName = $"{uniquePrefix}_{safeFileName}{ext}";

            var fullPath = Path.Combine(baseFolder, finalFileName);

            // ذخیره فایل
            using (var output = File.Create(fullPath))
            {
                await fileStream.CopyToAsync(output, 81920, cancellationToken);
            }

            // مسیر نسبی برمی‌گردانیم تا اگر RootPath عوض شد مشکلی نباشد
            var relativePath = Path.GetRelativePath(_options.RootPath, fullPath);
            return relativePath.Replace('\\', '/');
        }

        public Task<Stream> OpenReadAsync(
            string filePath,
            CancellationToken cancellationToken = default)
        {
            var fullPath = GetFullPath(filePath);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException("File not found", fullPath);

            // FileStream را به صورت Task برمی‌گردانیم
            Stream stream = new FileStream(
                fullPath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read);

            return Task.FromResult(stream);
        }

        public Task<bool> DeleteAsync(
            string filePath,
            CancellationToken cancellationToken = default)
        {
            var fullPath = GetFullPath(filePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public bool FileExists(string filePath)
        {
            var fullPath = GetFullPath(filePath);
            return File.Exists(fullPath);
        }

        private string GetFullPath(string relativePath)
        {
            if (Path.IsPathRooted(relativePath))
                return relativePath; // اگر اشتباهی مسیر کامل ذخیره شده بود

            return Path.Combine(_options.RootPath, relativePath.Replace('/', Path.DirectorySeparatorChar));
        }

        private static string MakeSafeFileName(string name)
        {
            // حذف کاراکترهای خطرناک
            var invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            var invalidRegex = new Regex($"[{invalidChars}]+");
            var safe = invalidRegex.Replace(name, "-");

            // trim کردن
            safe = safe.Trim('-', ' ');

            if (string.IsNullOrWhiteSpace(safe))
                return "file";

            // طول خیلی بلند را کوتاه می‌کنیم
            if (safe.Length > 80)
                safe = safe.Substring(0, 80);

            return safe;
        }
    }
}
