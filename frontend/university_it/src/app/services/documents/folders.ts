export interface FolderRequest {
    name: string;
    parentId: number;
}

export const createFolder = async (folderRequest: FolderRequest) => {
    await fetch(process.env.NEXT_PUBLIC_API_URL+"folders", {
        method: "POST",
        headers: {
            "content-type": "application/json"
        },
        body: JSON.stringify(folderRequest),
        credentials: "include",
        mode: "cors"
    });
}

export const getFolderWithChilds = async (id: number) => {
    const response = await fetch(process.env.NEXT_PUBLIC_API_URL+`folders/${id}`, {
        method: "GET",
        credentials: "include",
        mode: "cors"
    });

    return response.json();
}

export const updateFolder = async (id: number, folderRequest: FolderRequest) => {
    await fetch(process.env.NEXT_PUBLIC_API_URL+`folders/${id}`, {
        method: "PUT",
        headers: {
            "content-type": "application/json"
        },
        body: JSON.stringify(folderRequest),
        credentials: "include",
        mode: "cors"
    });
}

export const deleteFolder = async (id: number) => {
    await fetch(process.env.NEXT_PUBLIC_API_URL+`folders/${id}`, {
        method: "DELETE",
        credentials: "include",
        mode: "cors"
    });
}

export const getFolderDownloadRef = (id: number) => {
    return process.env.NEXT_PUBLIC_API_URL+`folders/download/`+String(id);
}