using UniversityIT.Core.Enums.FileStructure;

namespace UniversityIT.DataAccess.Entities.FileStructure
{
    public class FileEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public FileStorageType StorageType { get; set; }
        public string FileRefValue { get; set; } = string.Empty;
        public int ParentId { get; set; }
        public FolderEntity? Parent { get; set; }
        public string ParentPath { get; set; } = string.Empty;
    }
}