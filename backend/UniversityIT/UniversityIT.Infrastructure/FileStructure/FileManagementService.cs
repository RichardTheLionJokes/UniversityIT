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
                string ext = Path.GetExtension(Path.GetFileName(doc.FileName));
                string fullPath = "";
                do
                {
                    fullPath = Path.ChangeExtension(Path.Combine(folderPath, Path.GetRandomFileName()), ext);
                }
                while (File.Exists(fullPath) || Directory.Exists(fullPath));

                await using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await doc.CopyToAsync(stream);
                }

                return fullPath;
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.Message);
            }
        }

        public Result<string> DeleteFile(string fullPath)
        {
            try
            {
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }

                return fullPath;
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.Message);
            }
        }
    }
}