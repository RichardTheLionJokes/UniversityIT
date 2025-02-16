export interface FileRequest {
    name: string;
    parentId: number;
    file?: File;
}

export const uploadFile = async (fileRequest: FileRequest) => {
    const formData = new FormData();
    formData.append("name", fileRequest.name);
    formData.append("parentId", String(fileRequest.parentId));
    if (fileRequest.file != null) formData.append("file", fileRequest.file);

    const response = await fetch(process.env.NEXT_PUBLIC_API_URL+"files", {
        method: "POST",
        body: formData,
        credentials: "include",
        mode: "cors"
    });
    if (response.status === 500) console.log(response.text());
}

export const updateFile = async (id: number, fileRequest: FileRequest) => {
    const formData = new FormData();
    formData.append("name", fileRequest.name);
    formData.append("parentId", String(fileRequest.parentId));
    if (fileRequest.file != null) formData.append("file", fileRequest.file);

    const response = await fetch(process.env.NEXT_PUBLIC_API_URL+`files/${id}`, {
        method: "PUT",
        body: formData,
        credentials: "include",
        mode: "cors"
    });
    if (response.status === 400) console.log(response.text());
}

export const deleteFile = async (id: number) => {
    await fetch(process.env.NEXT_PUBLIC_API_URL+`files/${id}`, {
        method: "DELETE",
        credentials: "include",
        mode: "cors"
    });
}

export const getFileDownloadRef = (id: number) => {
    return process.env.NEXT_PUBLIC_API_URL+`files/download/`+String(id);
}