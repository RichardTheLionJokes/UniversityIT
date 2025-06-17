using CSharpFunctionalExtensions;

namespace UniversityIT.Core.Models.FileStructure
{
    public class FolderWithChilds
    {
        private FolderWithChilds(int id, string name, int? parentId, string parentPath)
        {
            Folder = FolderDto.Create(id, name, parentId, parentPath).Value;
        }

        public FolderDto Folder { get; }
        public List<FileStructureDto> Childs { get; set; } = [];

        public static Result<FolderWithChilds> Create(int id, string name, int? parentId, string parentPath)
        {
            var folder = new FolderWithChilds(id, name, parentId, parentPath);

            return Result.Success(folder);
        }
    }
}