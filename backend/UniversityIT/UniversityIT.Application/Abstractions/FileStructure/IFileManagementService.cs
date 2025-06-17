using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using UniversityIT.Core.Models.FileStructure;

namespace UniversityIT.Application.Abstractions.FileStructure
{
    public interface IFileManagementService
    {
        Task<Result<string>> SaveFile(IFormFile doc);
        Result<string> DeleteFile(string fullPath);
        Task<Result<byte[]>> ArchiveFolder(FolderWithChilds folderWithChilds);
    }
}
