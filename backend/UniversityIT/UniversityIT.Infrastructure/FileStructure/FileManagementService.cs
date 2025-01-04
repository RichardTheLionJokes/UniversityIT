using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using UniversityIT.Application.Abstractions.FileStructure;

namespace UniversityIT.Infrastructure.FileStructure
{
    public class FileManagementService : IFileManagementService
    {
        public async Task<Result<string>> SaveFile(IFormFile doc, string folderPath)
        {
            try
            {
                var fileName = Path.GetFileName(doc.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await doc.CopyToAsync(stream);
                }

                return filePath;
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.Message);
            }
        }
    }
}