export interface UserRequest {
    email: string;
    password: string;
    userName: string;
    fullName?: string;
    position?: string;
    phoneNumber?: string;
}

export interface ChangePassRequest {
    email: string;
    oldPassword: string;
    newPassword: string;
}

export const register = async (userRequest: UserRequest) => {
    await fetch(process.env.NEXT_PUBLIC_API_URL+"register", {
        method: "POST",
        headers: {
            "content-type": "application/json"
        },
        body: JSON.stringify(userRequest)
    });
}

export const login = async (email: string, password: string) => {
    const response = await fetch(process.env.NEXT_PUBLIC_API_URL+"login", {
        method: "POST",
        headers: {
            "content-type": "application/json"
        },
        credentials: "include",
        mode: "cors",
        body: JSON.stringify({ Email: email, Password: password })
    });

    return response.json();
}

export const logout = async () => {
    await fetch(process.env.NEXT_PUBLIC_API_URL+"logout", {
        method: "GET",
        credentials: "include",
        mode: "cors"
    });
}

export const changePassword = async (userRequest: ChangePassRequest) => {
    await fetch(process.env.NEXT_PUBLIC_API_URL+`changePassword`, {
        method: "POST",
        headers: {
            "content-type": "application/json"
        },
        body: JSON.stringify(userRequest),
        credentials: "include",
        mode: "cors"
    });
}

export const resetPassword = async (email: string) => {
    await fetch(process.env.NEXT_PUBLIC_API_URL+`resetPassword/${email}`, {
        method: "GET",
        credentials: "include",
        mode: "cors"
    });
}