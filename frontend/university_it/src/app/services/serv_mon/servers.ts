export interface ServerRequest {
    name: string;
    ipAddress: string;
    shortDescription: string;
    description: string;
    activity: boolean;
}

export const getAllServers = async () => {
    const response = await fetch(process.env.NEXT_PUBLIC_API_URL+"servers", {
        method: "GET",
        credentials: "include",
        mode: "cors"
    });

    return response.json();
}

export const createServer = async (serverRequest: ServerRequest) => {
    await fetch(process.env.NEXT_PUBLIC_API_URL+"servers", {
        method: "POST",
        headers: {
            "content-type": "application/json"
        },
        body: JSON.stringify(serverRequest),
        credentials: "include",
        mode: "cors"
    });
}

export const updateServer = async (id: string, serverRequest: ServerRequest) => {
    await fetch(process.env.NEXT_PUBLIC_API_URL+`servers/${id}`, {
        method: "PUT",
        headers: {
            "content-type": "application/json"
        },
        body: JSON.stringify(serverRequest),
        credentials: "include",
        mode: "cors"
    });
}

export const deleteServer = async (id: string) => {
    await fetch(process.env.NEXT_PUBLIC_API_URL+`servers/${id}`, {
        method: "DELETE",
        credentials: "include",
        mode: "cors"
    });
}

export const pingServer = async (id: string) => {
    await fetch(process.env.NEXT_PUBLIC_API_URL+`servers/ping/${id}`, {
        method: "GET",
        credentials: "include",
        mode: "cors"
    });
}