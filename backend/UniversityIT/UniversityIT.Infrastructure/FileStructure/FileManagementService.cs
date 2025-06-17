using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.IO.Compression;
using UniversityIT.Application.Abstractions.FileStructure;
using UniversityIT.Core.Models.FileStructure;

namespace UniversityIT.Infrastructure.FileStructure
{
    public class FileManagementService : IFileManagementService
    {
        private readonly StaticFilesOptions _staticFilesOptions;
        private string path;

        public FileManagementService(IOptions<StaticFilesOptions> options)
        {
            _staticFilesOptions = options.Value;
            path = _staticFilesOptions.GeneratePath();
        }

        public async Task<Result<string>> SaveFile(IFormFile doc)
        {
            try
            {
                string ext = Path.GetExtension(Path.GetFileName(doc.FileName));
                string fullPath = "";
                do
                {
                    fullPath = Path.ChangeExtension(Path.Combine(path, Path.GetRandomFileName()), ext);
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

        public async Task<Result<byte[]>> ArchiveFolder(FolderWithChilds folderWithChilds)
        {
            var folder = folderWithChilds.Folder;
            string zipName = folder.Name + ".zip";
            string zipPath = Path.Combine(new string[] { path, zipName });

            if (File.Exists(zipPath))
            {
                File.Delete(zipPath);
            }
            using (var zip = ZipFile.Open(zipPath, ZipArchiveMode.Create))
            {
                AddToZip(zip, folder.Id, "", folderWithChilds.Childs);
            }

            byte[] fileContent = await File.ReadAllBytesAsync(zipPath);
            DeleteFile(zipPath);

            return fileContent;
        }

        private void AddToZip(ZipArchive zip, int parentId, string parentPath, List<FileStructureDto> allChilds)
        {
            var childs = allChilds
                .Where(c => c.ParentId == parentId)
                .ToList();

            foreach (var child in childs)
            {
                string currentPath = Path.Combine(new string[] { parentPath, child.Name });
                if (child.IsFolder)
                {
                    zip.CreateEntry(currentPath + Path.DirectorySeparatorChar);
                    AddToZip(zip, child.Id, currentPath, allChilds);
                }
                else
                {
                    zip.CreateEntryFromFile(child.FileRefValue, currentPath + child.Extension);
                }
            }
        }
    }
}