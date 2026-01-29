using Microsoft.AspNetCore.Components.Forms;

namespace HseHackathon.Services;

public class FileUploadService
{
    private readonly string _uploadDirectory;
    private readonly ILogger<FileUploadService> _logger;
    private readonly long _maxFileSizeBytes = 10 * 1024 * 1024; // 10 MB
    private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

    public FileUploadService(IWebHostEnvironment environment, ILogger<FileUploadService> logger)
    {
        _uploadDirectory = Path.Combine(environment.WebRootPath, "uploads", "incidents");
        _logger = logger;
    }

    /// <summary>
    /// Upload a file to the server
    /// </summary>
    public async Task<(bool Success, string FilePath, string? ErrorMessage)> UploadFileAsync(IBrowserFile file)
    {
        try
        {
            // Validate file
            var validationError = ValidateFile(file);
            if (!string.IsNullOrEmpty(validationError))
            {
                return (false, string.Empty, validationError);
            }

            // Create directory if it doesn't exist
            if (!Directory.Exists(_uploadDirectory))
            {
                Directory.CreateDirectory(_uploadDirectory);
            }

            // Generate unique filename
            var fileName = $"{Guid.NewGuid()}_{file.Name}";
            var filePath = Path.Combine(_uploadDirectory, fileName);

            // Save file
            using (var stream = file.OpenReadStream(_maxFileSizeBytes))
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await stream.CopyToAsync(fileStream);
                }
            }

            var relativeUrl = $"/uploads/incidents/{fileName}";
            _logger.LogInformation($"File uploaded successfully: {fileName}");
            
            return (true, relativeUrl, null);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error uploading file: {ex.Message}");
            return (false, string.Empty, $"Error uploading file: {ex.Message}");
        }
    }

    /// <summary>
    /// Delete a file from the server
    /// </summary>
    public bool DeleteFile(string relativePath)
    {
        try
        {
            if (string.IsNullOrEmpty(relativePath))
                return false;

            var fileName = Path.GetFileName(relativePath);
            var filePath = Path.Combine(_uploadDirectory, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                _logger.LogInformation($"File deleted successfully: {fileName}");
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error deleting file: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Validate file before upload
    /// </summary>
    private string? ValidateFile(IBrowserFile file)
    {
        if (file == null)
            return "File is required";

        if (file.Size > _maxFileSizeBytes)
            return $"File size exceeds maximum allowed size of {_maxFileSizeBytes / (1024 * 1024)} MB";

        var fileExtension = Path.GetExtension(file.Name).ToLower();
        if (!_allowedExtensions.Contains(fileExtension))
            return "Only image files (.jpg, .jpeg, .png, .gif) are allowed";

        return null;
    }
}
