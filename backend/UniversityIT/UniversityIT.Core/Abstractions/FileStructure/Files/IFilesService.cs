﻿using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using UniversityIT.Core.Models.FileStructure;

namespace UniversityIT.Core.Abstractions.FileStructure.Files
{
    public interface IFilesService
    {
        Task<Result<int>> CreateFile(IFormFile doc, FileDto file);
        Task<(byte[], string?, string)> DownloadFile(int id);
        Task<FileDto> GetFileById(int id);
        Task<int> UpdateFile(int id, string name);
        Task<int> DeleteFile(int id);        
    }
}