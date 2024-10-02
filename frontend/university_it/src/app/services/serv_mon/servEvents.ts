export const getAllServEvents = async () => {
    const response = await fetch(process.env.NEXT_PUBLIC_API_URL+"ServEvents", {
        credentials: "include",
        mode: "cors"
    });

    return response.json();
}