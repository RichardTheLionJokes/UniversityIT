using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using UniversityIT.Application.Abstractions.FileStructure;
using UniversityIT.Core.Abstractions.FileStructure.Files;
using UniversityIT.Core.Models.FileStructure;

namespace UniversityIT.Application.Services.FileStructure
{
    public class FilesService : IFilesService
    {
        private readonly IFilesRepository _filesRepository;
        private readonly IFileManagementService _fileManagementService;

        public FilesService(IFilesRepository filesRepository, IFileManagementService fileManagementService)
        {
            _filesRepository = filesRepository;
            _fileManagementService = fileManagementService;
        }

        public async Task<Result<int>> CreateFile(IFormFile doc, FileDto file, string folderPath)
        {
            var fileRefValue = await _fileManagementService.SaveFile(doc, folderPath);
            if (fileRefValue.IsFailure)
                return Result.Failure<int>(fileRefValue.Error);

            file.SetFileRef(fileRefValue.Value);
            int fileId = await _filesRepository.Create(file);

            return Result.Success(fileId);
        }
    }
}