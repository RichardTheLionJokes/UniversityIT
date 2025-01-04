using UniversityIT.Core.Abstractions.FileStructure.Files;
using UniversityIT.Core.Models.FileStructure;

namespace UniversityIT.DataAccess.Repositories.FileStructure
{
    public class FilesRepository : IFilesRepository
    {
        private readonly UniversityITDbContext _context;

        public FilesRepository(UniversityITDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(FileDto file)
        {
            var fileEntity = DataBaseMappings.FileToEntity(file);

            await _context.Files.AddAsync(fileEntity);
            await _context.SaveChangesAsync();

            return fileEntity.Id;
        }
    }
}