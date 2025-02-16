using Microsoft.AspNetCore.Mvc;
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
            var folder = await foldersService.GetFolderWithChilds(id);

            var response = new FoldersWithChildsResponse(
                folder.Id,
                folder.Name,
                folder.ParentId,
            new List<FileStructuresResponse>());

            foreach (var child in folder.Childs)
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
    }
}