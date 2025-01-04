namespace UniversityIT.API.Contracts.FileStructure.Folders
{
    public record FileStructuresResponse(
        int Id,
        string Name,
        string Extention,
        bool IsFolder,
        int? ParentId);
}