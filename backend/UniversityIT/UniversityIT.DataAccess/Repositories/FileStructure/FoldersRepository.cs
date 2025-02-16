using Microsoft.EntityFrameworkCore;
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

            string parentPath = await GetParentPath(folder.ParentId);
            folderEntity.ParentPath = parentPath;

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

        //public async Task<List<FileStructureDto>> GetChilds(int id)
        //{
        //    var childsFolders = await _context.Folders
        //        .AsNoTracking()
        //        .Where(fr => fr.ParentId == id)
        //        .OrderBy(fr => fr.Name)
        //        .ToListAsync();

        //    var childsFiles = await _context.Files
        //        .AsNoTracking()
        //        .Where(fl => fl.ParentId == id)
        //        .OrderBy(fl => fl.Name)
        //        .ToListAsync();

        //    var result = childsFolders
        //        .Select(fr => DataBaseMappings.FileStructureDtoFromFolderEntity(fr))
        //        .Union(childsFiles
        //        .Select(fl => DataBaseMappings.FileStructureDtoFromFileEntity(fl)))
        //        .ToList();

        //    return result;
        //}

        public async Task<List<FileDto>> GetAllLevelChildsFiles(int id)
        {
            var folder = await _context.Folders
                .AsNoTracking()
                .FirstOrDefaultAsync(fr => fr.Id == id) ?? throw new Exception();

            var allLevelChildsFiles = await _context.Files
                .AsNoTracking()
                .Where(fl => fl.ParentPath.StartsWith(folder.ParentPath))
                .OrderBy(fl => fl.Name)
                .ToListAsync();

            var result = allLevelChildsFiles
                .Select(fl => DataBaseMappings.FileFromEntity(fl))
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

        private async Task<string> GetParentPath(int? parentId)
        {
            string parentPath = "/";
            if (parentId is not null)
            {
                var parentEntity = await _context.Folders
                    .AsNoTracking()
                    .FirstOrDefaultAsync(f => f.Id == parentId) ?? throw new Exception();

                parentPath = parentEntity.ParentPath + parentEntity.Id.ToString() + parentPath;
            }

            return parentPath;
        }
    }
}