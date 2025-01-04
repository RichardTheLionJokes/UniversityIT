using UniversityIT.Core.Models.FileStructure;

namespace UniversityIT.Core.Abstractions.FileStructure.Folders
{
    public interface IFoldersRepository
    {
        Task<int> Create(FolderDto folder);
        Task<FolderWithChilds> GetFolderWithChilds(int id);
        Task<List<FileStructureDto>> GetChilds(int id);
        Task<int> Update(int id, string name);
        Task<int> Delete(int id);
    }
}