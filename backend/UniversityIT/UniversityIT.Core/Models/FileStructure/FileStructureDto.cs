using CSharpFunctionalExtensions;

namespace UniversityIT.Core.Models.FileStructure
{
    public class FileStructureDto
    {
        private FileStructureDto(int id, string name, string extension, bool isFolder, int? parentId)
        {
            Id = id;
            Name = name;
            Extension = extension;
            IsFolder = isFolder;
            ParentId = parentId;
        }

        public int Id { get; }
        public string Name { get; } = string.Empty;
        public string Extension { get; } = string.Empty;
        public bool IsFolder { get; }
        public int? ParentId { get; }

        public static Result<FileStructureDto> Create(int id, string name, string extension, bool isFolder, int? parentId)
        {
            var folder = new FileStructureDto(id, name, extension, isFolder, parentId);

            return Result.Success(folder);
        }
    }
}