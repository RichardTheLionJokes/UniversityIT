using Microsoft.EntityFrameworkCore;
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

            string parentPath = await GetParentPath(file.ParentId);
            fileEntity.ParentPath = parentPath;

            await _context.Files.AddAsync(fileEntity);

            await _context.SaveChangesAsync();

            return fileEntity.Id;
        }

        public async Task<FileDto> GetById(int id)
        {
            var fileEntity = await _context.Files
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id) ?? throw new Exception();

            return DataBaseMappings.FileFromEntity(fileEntity);
        }

        public async Task<int> Update(int id, string name)
        {
            await _context.Files
            .Where(f => f.Id == id)
            .ExecuteUpdateAsync(spc => spc
                    .SetProperty(t => t.Name, t => name));

            return id;
        }

        public async Task<int> Delete(int id)
        {
            await _context.Files
                .Where(f => f.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        private async Task<string> GetParentPath(int parentId)
        {
            string parentPath = "/";
            var parentEntity = await _context.Folders
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == parentId) ?? throw new Exception();

            parentPath = parentEntity.ParentPath + parentEntity.Id.ToString() + parentPath;

            return parentPath;
        }
    }
}