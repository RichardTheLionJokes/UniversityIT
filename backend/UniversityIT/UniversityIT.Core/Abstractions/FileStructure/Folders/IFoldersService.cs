using UniversityIT.Core.Models.FileStructure;

namespace UniversityIT.Core.Abstractions.FileStructure.Folders
{
    public interface IFoldersService
    {
        Task<int> CreateFolder(FolderDto folder);
        Task<FolderWithChilds> GetFolderWithChilds(int id);
        Task<List<FileStructureDto>> GetFolderChilds(int folderId);
        Task<int> UpdateFolder(int id, string name);
        Task<int> DeleteFolder(int id);
    }
}