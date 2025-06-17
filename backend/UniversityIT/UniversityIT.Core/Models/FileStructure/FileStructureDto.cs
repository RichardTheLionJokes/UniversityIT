using CSharpFunctionalExtensions;

namespace UniversityIT.Core.Models.FileStructure
{
    public class FileStructureDto
    {
        private FileStructureDto(int id, string name, string extension, bool isFolder, string fileRefValue, int? parentId, string parentPath)
        {
            Id = id;
            Name = name;
            Extension = extension;
            IsFolder = isFolder;
            FileRefValue = fileRefValue;
            ParentId = parentId;
            ParentPath = parentPath;
        }

        public int Id { get; }
        public string Name { get; } = string.Empty;
        public string Extension { get; } = string.Empty;
        public bool IsFolder { get; }
        public string FileRefValue { get; set; } = string.Empty;
        public int? ParentId { get; }
        public string ParentPath { get; set; } = string.Empty;

        public static Result<FileStructureDto> Create(int id, string name, string extension, bool isFolder, string fileRefValue, int? parentId, string parentPath)
        {
            var folder = new FileStructureDto(id, name, extension, isFolder, fileRefValue, parentId, parentPath);

            return Result.Success(folder);
        }
    }
}