using UniversityIT.Core.Abstractions.FileStructure.Folders;
using UniversityIT.Core.Models.FileStructure;

namespace UniversityIT.Application.Services.FileStructure
{
    public class FoldersService : IFoldersService
    {
        private readonly IFoldersRepository _foldersRepository;

        public FoldersService(IFoldersRepository foldersRepository)
        {
            _foldersRepository = foldersRepository;
        }

        public async Task<int> CreateFolder(FolderDto folder)
        {
            int folderId = await _foldersRepository.Create(folder);

            return folderId;
        }

        public async Task<FolderWithChilds> GetFolderWithChilds(int id)
        {
            return await _foldersRepository.GetFolderWithChilds(id);
        }

        public async Task<List<FileStructureDto>> GetFolderChilds(int id)
        {
            return await _foldersRepository.GetChilds(id);
        }

        public async Task<int> UpdateFolder(int id, string name)
        {
            return await _foldersRepository.Update(id, name);
        }

        public async Task<int> DeleteFolder(int id)
        {
            return await _foldersRepository.Delete(id);
        }
    }
}