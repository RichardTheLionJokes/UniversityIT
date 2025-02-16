using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;

namespace UniversityIT.Application.Abstractions.FileStructure
{
    public interface IFileManagementService
    {
        Task<Result<string>> SaveFile(IFormFile doc, string path);
        Result<string> DeleteFile(string fullPath);
    }
}
