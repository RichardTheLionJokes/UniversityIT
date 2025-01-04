namespace UniversityIT.DataAccess.Entities.FileStructure
{
    public class FolderEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public FolderEntity? Parent { get; set; }
        public ICollection<FolderEntity> Folders { get; set; } = [];
        public ICollection<FileEntity> Files { get; set; } = [];
    }
}