namespace UniversityIT.API.Contracts.FileStructure.Files
{
    public record FilesRequest(
        IFormFile File,
        string Name,
        int ParentId);
}