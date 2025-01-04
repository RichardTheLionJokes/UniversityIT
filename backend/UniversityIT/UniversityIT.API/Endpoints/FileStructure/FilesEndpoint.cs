using Microsoft.AspNetCore.Mvc;
using UniversityIT.API.Contracts.FileStructure.Files;
using UniversityIT.API.Extentions;
using UniversityIT.Core.Abstractions.FileStructure.Files;
using UniversityIT.Core.Enums.Auth;
using UniversityIT.Core.Enums.FileStructure;
using UniversityIT.Core.Models.FileStructure;

namespace UniversityIT.API.Endpoints.FileStructure
{
    public static class FilesEndpoint
    {
        private static readonly string _staticFilesPath =
            Path.Combine(new string[] {Directory.GetCurrentDirectory(), "StaticFiles", "Documents"});

        public static IEndpointRouteBuilder MapFilesEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup("files")
                .RequireAuthorization()
                .DisableAntiforgery();

            endpoints.MapPost(string.Empty, CreateFile).RequirePermissions(Permission.Create);

            return endpoints;
        }

        private static async Task<IResult> CreateFile(
            [FromForm] FilesRequest request,
            IFilesService filesService)
        {
            var file = FileDto.Create(
                -1,
                request.Name,
                DateTime.Now.ToUniversalTime(),
                FileStorageType.Path,
                "",
                request.ParentId
                );

            var fileId = await filesService.CreateFile(request.File, file.Value, _staticFilesPath);

            return Results.Ok(fileId.Value);
        }
    }
}