using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using UniversityIT.Core.Models.FileStructure;

namespace UniversityIT.Core.Abstractions.FileStructure.Files
{
    public interface IFilesService
    {
        Task<Result<int>> CreateFile(IFormFile doc, FileDto file, string folderPath);
    }
}