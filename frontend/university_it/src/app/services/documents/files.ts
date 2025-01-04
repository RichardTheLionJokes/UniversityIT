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

    const response = await await fetch(process.env.NEXT_PUBLIC_API_URL+"files", {
        method: "POST",
        //headers: {
            //"content-type": "application/json"
        //},
        body: formData,
        credentials: "include",
        mode: "cors"
    });
    if (response.status === 500) console.log(response.text());
}