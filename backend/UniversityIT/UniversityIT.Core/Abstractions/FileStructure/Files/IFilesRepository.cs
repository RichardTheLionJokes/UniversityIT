using UniversityIT.Core.Models.FileStructure;

namespace UniversityIT.Core.Abstractions.FileStructure.Files
{
    public interface IFilesRepository
    {
        Task<int> Create(FileDto file);
    }
}