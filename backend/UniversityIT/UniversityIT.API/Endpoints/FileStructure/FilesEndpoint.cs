using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
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

            endpoints.MapGet("download/{id:int}", DownloadFile).RequirePermissions(Permission.Read);

            endpoints.MapPut("{id:int}", UpdateFile).RequirePermissions(Permission.Update);

            endpoints.MapDelete("{id:int}", DeleteFile).RequirePermissions(Permission.Delete);

            return endpoints;
        }

        private static async Task<IResult> CreateFile(
            [FromForm] FilesRequest request,
            IFilesService filesService)
        {
            if (request.Files.Count > 0)
            {
                var file = FileDto.Create(
                    -1,
                    request.Name,
                    DateTime.Now.ToUniversalTime(),
                    FileStorageType.Path,
                    "",
                    request.ParentId,
                    ""
                    );

                var fileId = await filesService.CreateFile(request.Files[0], file.Value, _staticFilesPath);

                return Results.Ok(fileId.Value);
            }
            else
                return Results.Problem("Not found file for upload");
        }

        private static async Task<IResult> DownloadFile(int id, IFilesService filesService, HttpContext context)
        {
            var (fileContent, contentType, fileName) = await filesService.DownloadFile(id);

            string contentDisposition;
            if (contentType != null && (contentType.StartsWith("image") || contentType == "application/pdf"))
                contentDisposition = "inline";
            else contentDisposition = "attachment";

            var contentDispositionHeader = new ContentDispositionHeaderValue(contentDisposition)
            {
                FileName = fileName
            };
            context.Response.Headers.ContentDisposition = contentDispositionHeader.ToString();

            return Results.File(fileContent, contentType);
        }

        private static async Task<IResult> UpdateFile(int id, [FromForm] FilesRequest request, IFilesService filesService)
        {
            var fileId = await filesService.UpdateFile(
                id,
                request.Name);

            return Results.Ok(fileId);
        }

        private static async Task<IResult> DeleteFile(int id, IFilesService filesService)
        {
            return Results.Ok(await filesService.DeleteFile(id));
        }
    }
}