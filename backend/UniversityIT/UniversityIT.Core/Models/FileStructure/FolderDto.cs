using CSharpFunctionalExtensions;

namespace UniversityIT.Core.Models.FileStructure
{
    public class FolderDto
    {
        private FolderDto(int id, string name, int? parentId)
        {
            Id = id;
            Name = name;
            ParentId = parentId;
        }

        public int Id { get; }
        public string Name { get; } = string.Empty;
        public int? ParentId { get; }

        public static Result<FolderDto> Create(int id, string name, int? parentId)
        {
            var folder = new FolderDto(id, name, parentId);

            return Result.Success(folder);
        }
    }
}