using UniversityIT.Core.Models.FileStructure;

namespace UniversityIT.Core.Abstractions.FileStructure.Files
{
    public interface IFilesRepository
    {
        Task<int> Create(FileDto file);
        Task<FileDto> GetById(int id);
        Task<int> Update(int id, string name);
        Task<int> Delete(int id);
    }
}