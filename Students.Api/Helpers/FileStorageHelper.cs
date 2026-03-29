namespace Students.Api.Helpers
{
    public static class FileStorageHelper
    {
        private static readonly string[] AllowedImageTypes =
        {
            "image/jpeg",
            "image/png"
        };

        private static readonly string[] AllowedDocTypes =
        {
            "application/pdf",
            "image/jpeg",
            "image/png"
        };

        // Saves a single file to wwwroot/{folder} and returns relative path
        public static async Task<(bool Ok, string? SavedPath, string? Error)> SaveFileAsync(
            IFormFile? file,
            string folder,
            long maxBytes,
            IWebHostEnvironment env,
            CancellationToken cancellationToken = default)
        {
            if (file == null || file.Length == 0)
                return (true, null, null); // No file is allowed

            // Basic checks
            var isDoc = AllowedDocTypes.Contains(file.ContentType);
            if (!isDoc)
                return (false, null, "Invalid file type. Only PDF/JPG/PNG allowed.");

            if (file.Length > maxBytes)
                return (false, null, $"File too large. Max {maxBytes / (1024 * 1024)} MB.");

            // Always save inside wwwroot/{folder}
            var webRoot = env.WebRootPath;
            if (string.IsNullOrWhiteSpace(webRoot))
            {
                // fallback: use ContentRootPath/wwwroot
                var contentRoot = env.ContentRootPath ?? Directory.GetCurrentDirectory();
                webRoot = Path.Combine(contentRoot, "wwwroot");
            }

            var root = Path.Combine(webRoot, folder);
            Directory.CreateDirectory(root);

            var ext = Path.GetExtension(file.FileName);
            var safeName = Guid.NewGuid().ToString("N") + ext;
            var fullPath = Path.Combine(root, safeName);

            try
            {
                using var readStream = file.OpenReadStream();
                if (!IsAllowedFileContent(readStream, file.ContentType, ext))
                    return (false, null, "File content does not match the declared file type.");

                using var stream = new FileStream(fullPath, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                await file.CopyToAsync(stream, cancellationToken);
            }
            catch (IOException ioEx)
            {
                return (false, null, "File write error: " + ioEx.Message);
            }
            catch (OperationCanceledException)
            {
                return (false, null, "File upload canceled.");
            }
            catch (Exception ex)
            {
                return (false, null, "Unexpected file save error: " + ex.Message);
            }

            // relative path from web root (for URLs)
            var relative = Path.Combine(folder, safeName).Replace("\\", "/");
            return (true, relative, null);
        }

        // Read initial bytes and verify signatures for PDF, JPG, PNG
        private static bool IsAllowedFileContent(Stream stream, string contentType, string extension)
        {
            if (stream == null || !stream.CanRead)
                return false;

            long originalPos = 0;
            try
            {
                originalPos = stream.CanSeek ? stream.Position : 0;
                byte[] header = new byte[8];
                int read = stream.Read(header, 0, header.Length);
                if (stream.CanSeek) stream.Position = originalPos;

                // PDF: %PDF
                if (read >= 4 && header[0] == 0x25 && header[1] == 0x50 && header[2] == 0x44 && header[3] == 0x46)
                    return true;

                // JPEG: FF D8 FF
                if (read >= 3 && header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF)
                    return true;

                // PNG: 89 50 4E 47 0D 0A 1A 0A
                if (read >= 8 && header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47
                    && header[4] == 0x0D && header[5] == 0x0A && header[6] == 0x1A && header[7] == 0x0A)
                    return true;

                return false;
            }
            catch
            {
                if (stream.CanSeek) stream.Position = originalPos;
                return false;
            }
        }
    }
}
