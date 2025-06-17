using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using MimeDetective;
using UniversityIT.Application.Abstractions.FileStructure;
using UniversityIT.Core.Abstractions.FileStructure.Files;
using UniversityIT.Core.Models.FileStructure;

namespace UniversityIT.Application.Services.FileStructure
{
    public class FilesService : IFilesService
    {
        private readonly IFilesRepository _filesRepository;
        private readonly IFileManagementService _fileManagementService;

        private readonly string[] imagesExts = { ".bmp", ".gif", ".jpeg", ".png" };

        public FilesService(IFilesRepository filesRepository, IFileManagementService fileManagementService)
        {
            _filesRepository = filesRepository;
            _fileManagementService = fileManagementService;
        }

        public async Task<Result<int>> CreateFile(IFormFile doc, FileDto file)
        {
            var fileRefValue = await _fileManagementService.SaveFile(doc);
            if (fileRefValue.IsFailure)
                return Result.Failure<int>(fileRefValue.Error);

            file.SetFileRef(fileRefValue.Value);
            int fileId = await _filesRepository.Create(file);

            return Result.Success(fileId);
        }

        public async Task<(byte[], string?, string)> DownloadFile(int id)
        {
            var file = await _filesRepository.GetById(id);
            byte[] fileContent = await File.ReadAllBytesAsync(file.FileRefValue);

            string extension = Path.GetExtension(file.FileRefValue);

            string? contentType = GetContentType(fileContent);

            return (fileContent, contentType, file.Name + (String.IsNullOrEmpty(extension) ? "" : extension));
        }

        public async Task<FileDto> GetFileById(int id)
        {
            return await _filesRepository.GetById(id);
        }

        public async Task<int> UpdateFile(int id, string name)
        {
            return await _filesRepository.Update(id, name);
        }

        public async Task<int> DeleteFile(int id)
        {
            var file = await GetFileById(id);
            var fileRefValue = _fileManagementService.DeleteFile(file.FileRefValue);

            return await _filesRepository.Delete(id);
        }

        private string? GetContentType(byte[] fileContent)
        {
            string contentType = "application/octet-stream";

            var Inspector = new ContentInspectorBuilder()
            {
                Definitions = MimeDetective.Definitions.DefaultDefinitions.All()
            }.Build();

            var results = Inspector.Inspect(fileContent);
            if (results.Length == 1)
            {
                contentType = results[0].Definition.File.MimeType ?? contentType;
            }

            return contentType;
        }
    }
}