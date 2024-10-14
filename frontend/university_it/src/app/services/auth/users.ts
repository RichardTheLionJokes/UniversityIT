export interface UserRequest {
    email: string;
    userName: string;
    fullName?: string;
    position?: string;
    phoneNumber?: string;
}

export const getUserByEmail = async (email: string) => {
    const response = await fetch(process.env.NEXT_PUBLIC_API_URL+`users/${email}`, {
        method: "GET",
        credentials: "include",
        mode: "cors"
    });

    return response.json();
}

export const updateUser = async (id: string, userRequest: UserRequest) => {
    await fetch(process.env.NEXT_PUBLIC_API_URL+`users/${id}`, {
        method: "PUT",
        headers: {
            "content-type": "application/json"
        },
        body: JSON.stringify(userRequest),
        credentials: "include",
        mode: "cors"
    });
}