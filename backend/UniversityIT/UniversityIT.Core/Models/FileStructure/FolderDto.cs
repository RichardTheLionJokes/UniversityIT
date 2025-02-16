using CSharpFunctionalExtensions;

namespace UniversityIT.Core.Models.FileStructure
{
    public class FolderDto
    {
        private FolderDto(int id, string name, int? parentId, string parentPath)
        {
            Id = id;
            Name = name;
            ParentId = parentId;
            ParentPath = parentPath;
        }

        public int Id { get; }
        public string Name { get; } = string.Empty;
        public int? ParentId { get; }
        public string ParentPath { get; set; } = string.Empty;

        public static Result<FolderDto> Create(int id, string name, int? parentId, string parentPath)
        {
            var folder = new FolderDto(id, name, parentId, parentPath);

            return Result.Success(folder);
        }
    }
}