using CSharpFunctionalExtensions;
using UniversityIT.Core.Enums.FileStructure;

namespace UniversityIT.Core.Models.FileStructure
{
    public class FileDto
    {
        private FileDto(int id, string name, DateTime createdAt, FileStorageType storageType, string fileRefValue, int parentId, string parentPath)
        {
            Id = id;
            Name = name;
            CreatedAt = createdAt;
            StorageType = storageType;
            FileRefValue = fileRefValue;
            ParentId = parentId;
            ParentPath = parentPath;
        }

        public int Id { get; }
        public string Name { get; } = string.Empty;
        public DateTime CreatedAt { get; }
        public FileStorageType StorageType { get; }
        public string FileRefValue { get; set; } = string.Empty;
        public int ParentId { get; }
        public string ParentPath { get; set; } = string.Empty;

        public static Result<FileDto> Create(int id, string name, DateTime createdAt, FileStorageType storageType, string fileRefValue, int parentId, string parentPath)
        {
            var file = new FileDto(id, name, createdAt, storageType, fileRefValue, parentId, parentPath);

            return Result.Success(file);
        }

        public void SetFileRef(string fileRefValue)
        {
            FileRefValue = fileRefValue;
        }
    }
}