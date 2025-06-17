using System.IO.Compression;
using UniversityIT.Application.Abstractions.FileStructure;
using UniversityIT.Core.Abstractions.FileStructure.Files;
using UniversityIT.Core.Abstractions.FileStructure.Folders;
using UniversityIT.Core.Models.FileStructure;

namespace UniversityIT.Application.Services.FileStructure
{
    public class FoldersService : IFoldersService
    {
        private readonly IFoldersRepository _foldersRepository;
        private readonly IFilesService _filesService;
        private readonly IFileManagementService _fileManagementService;

        public FoldersService(IFoldersRepository foldersRepository, IFilesService filesService, IFileManagementService fileManagementService)
        {
            _foldersRepository = foldersRepository;
            _filesService = filesService;
            _fileManagementService = fileManagementService;
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

        //public async Task<List<FileStructureDto>> GetFolderChilds(int id)
        //{
        //    return await _foldersRepository.GetChilds(id);
        //}

        public async Task<int> UpdateFolder(int id, string name)
        {
            return await _foldersRepository.Update(id, name);
        }

        public async Task<int> DeleteFolder(int id)
        {
            var files = await _foldersRepository.GetAllLevelChildsFiles(id);
            foreach (var file in files)
            {
                await _filesService.DeleteFile(file.Id);
            }

            return await _foldersRepository.Delete(id);
        }

        public async Task<(byte[], string)> DownloadFolder(int id)
        {
            var folderWithChilds = await _foldersRepository.GetFolderWithAllLevelChilds(id);

            var fileContent = await _fileManagementService.ArchiveFolder(folderWithChilds);

            return (fileContent.Value, folderWithChilds.Folder.Name + ".zip");
        }
    }
}