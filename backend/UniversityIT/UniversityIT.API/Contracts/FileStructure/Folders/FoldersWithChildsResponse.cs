namespace UniversityIT.API.Contracts.FileStructure.Folders
{
    public record FoldersWithChildsResponse(
        int Id,
        string Name,
        int? ParentId,
        List<FileStructuresResponse> Childs);
}