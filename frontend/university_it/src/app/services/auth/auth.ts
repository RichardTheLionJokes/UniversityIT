export const Login = async (email: string, password: string) => {
    const response = await fetch(process.env.NEXT_PUBLIC_API_URL+"Login", {
        method: "POST",
        headers: {
            "content-type": "application/json"
        }, credentials: "include", mode: "cors",
        body: JSON.stringify({ Email: email, Password: password })
    });

    return response.json();
}