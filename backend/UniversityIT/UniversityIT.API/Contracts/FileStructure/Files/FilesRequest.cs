namespace UniversityIT.API.Contracts.FileStructure.Files
{
    public record FilesRequest(
        string Name,
        int ParentId,
        IFormFileCollection Files);
}