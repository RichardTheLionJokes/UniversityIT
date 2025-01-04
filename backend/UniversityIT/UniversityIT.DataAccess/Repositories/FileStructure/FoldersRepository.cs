using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using UniversityIT.Core.Abstractions.FileStructure.Folders;
using UniversityIT.Core.Models.FileStructure;

namespace UniversityIT.DataAccess.Repositories.FileStructure
{
    public class FoldersRepository : IFoldersRepository
    {
        private readonly UniversityITDbContext _context;

        public FoldersRepository(UniversityITDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(FolderDto folder)
        {
            var folderEntity = DataBaseMappings.FolderToEntity(folder);

            await _context.Folders.AddAsync(folderEntity);
            await _context.SaveChangesAsync();

            return folderEntity.Id;
        }

        public async Task<FolderWithChilds> GetFolderWithChilds(int id)
        {
            var folderEntity = await _context.Folders
                .AsNoTracking()
                .Include(fl => fl.Folders)
                .Include(fl => fl.Files)
                .Where(fl => fl.Id == id)
                .FirstOrDefaultAsync();

            if (folderEntity != null)
            {
                var folder = FolderWithChilds.Create(
                    folderEntity.Id,
                    folderEntity.Name,
                    folderEntity.ParentId
                ).Value;

                foreach (var child in folderEntity.Folders)
                {
                    folder.Childs.Add(DataBaseMappings.FileStructureDtoFromFolderEntity(child));
                }
                foreach (var child in folderEntity.Files)
                {
                    folder.Childs.Add(DataBaseMappings.FileStructureDtoFromFileEntity(child));
                }

                return folder;
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<List<FileStructureDto>> GetChilds(int id)
        {
            var childsFolders = await _context.Folders
                .AsNoTracking()
                .Where(fr => fr.ParentId == id)
                .OrderBy(fr => fr.Name)
                .ToListAsync();

            var childsFiles = await _context.Files
                .AsNoTracking()
                .Where(fl => fl.ParentId == id)
                .OrderBy(fl => fl.Name)
                .ToListAsync();

            var result = childsFolders
                .Select(fr => DataBaseMappings.FileStructureDtoFromFolderEntity(fr))
                .Union(childsFiles
                .Select(fl => DataBaseMappings.FileStructureDtoFromFileEntity(fl)))
                .ToList();

            return result;
        }

        public async Task<int> Update(int id, string name)
        {
            await _context.Folders
            .Where(f => f.Id == id)
            .ExecuteUpdateAsync(spc => spc
                    .SetProperty(t => t.Name, t => name));

            return id;
        }

        public async Task<int> Delete(int id)
        {
            await _context.Folders
                .Where(f => f.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}