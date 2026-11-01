
namespace FileRecords.Models;

public class FileRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string OriginalName { get; set; }

    public required string StorageName { get; set; }

    public required string ContentType { get; set; }

    public long Size { get; set; }

    public DateTime UploadedAtUtc { get; set; } = DateTime.UtcNow;
}