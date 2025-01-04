using CSharpFunctionalExtensions;

namespace UniversityIT.Core.Models.FileStructure
{
    public class FolderWithChilds
    {
        private FolderWithChilds(int id, string name, int? parentId)
        {
            Id = id;
            Name = name;
            ParentId = parentId;
        }

        public int Id { get; }
        public string Name { get; } = string.Empty;
        public int? ParentId { get; }
        public List<FileStructureDto> Childs { get; set; } = [];

        public static Result<FolderWithChilds> Create(int id, string name, int? parentId)
        {
            var folder = new FolderWithChilds(id, name, parentId);

            return Result.Success(folder);
        }
    }
}
