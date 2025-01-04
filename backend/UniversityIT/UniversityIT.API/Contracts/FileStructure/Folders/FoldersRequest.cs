namespace UniversityIT.API.Contracts.FileStructure.Folders
{
    public record FoldersRequest(
        string Name,
        int ParentId);
}