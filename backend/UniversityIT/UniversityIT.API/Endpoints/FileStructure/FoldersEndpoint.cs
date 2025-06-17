using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using UniversityIT.API.Contracts.FileStructure.Folders;
using UniversityIT.API.Extentions;
using UniversityIT.Core.Abstractions.FileStructure.Folders;
using UniversityIT.Core.Enums.Auth;
using UniversityIT.Core.Models.FileStructure;

namespace UniversityIT.API.Endpoints.FileStructure
{
    public static class FoldersEndpoint
    {
        public static IEndpointRouteBuilder MapFoldersEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup("folders")
                .RequireAuthorization()
                .DisableAntiforgery();

            endpoints.MapPost(string.Empty, CreateFolder).RequirePermissions(Permission.Create);

            endpoints.MapGet("{id:int}", GetFolderWithChilds).RequirePermissions(Permission.Read);

            endpoints.MapPut("{id:int}", UpdateFolder).RequirePermissions(Permission.Update);

            endpoints.MapDelete("{id:int}", DeleteFolder).RequirePermissions(Permission.Delete);

            endpoints.MapGet("download/{id:int}", DownloadFolder).RequirePermissions(Permission.Read);

            return endpoints;
        }

        private static async Task<IResult> CreateFolder(
            [FromBody] FoldersRequest request,
            IFoldersService foldersService)
        {
            var folder = FolderDto.Create(
                -1,
                request.Name,
                request.ParentId,
                ""
                );

            var folderId = await foldersService.CreateFolder(folder.Value);

            return Results.Ok(folderId);
        }

        private static async Task<IResult> GetFolderWithChilds(int id, IFoldersService foldersService)
        {
            var folderWithChilds = await foldersService.GetFolderWithChilds(id);
            var folder = folderWithChilds.Folder;

            var response = new FoldersWithChildsResponse(
                folder.Id,
                folder.Name,
                folder.ParentId,
            new List<FileStructuresResponse>());

            foreach (var child in folderWithChilds.Childs)
            {
                response.Childs.Add(new FileStructuresResponse(
                    child.Id,
                    child.Name,
                    child.Extension,
                    child.IsFolder,
                    child.ParentId));
            }

            return Results.Ok(response);
        }

        private static async Task<IResult> UpdateFolder(int id, [FromBody] FoldersRequest request, IFoldersService foldersService)
        {
            var folderId = await foldersService.UpdateFolder(
                id,
                request.Name);

            return Results.Ok(folderId);
        }

        private static async Task<IResult> DeleteFolder(int id, IFoldersService foldersService)
        {
            return Results.Ok(await foldersService.DeleteFolder(id));
        }

        private static async Task<IResult> DownloadFolder(int id, IFoldersService foldersService, HttpContext context)
        {
            var (zipContent, fileName) = await foldersService.DownloadFolder(id);

            string contentDisposition = "inline";

            var contentDispositionHeader = new ContentDispositionHeaderValue(contentDisposition)
            {
                FileName = fileName
            };
            context.Response.Headers.ContentDisposition = contentDispositionHeader.ToString();

            return Results.File(zipContent, contentDisposition);
        }
    }
}