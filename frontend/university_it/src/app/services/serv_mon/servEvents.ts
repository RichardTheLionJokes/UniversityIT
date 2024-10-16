export const getAllServEvents = async () => {
    const response = await fetch(process.env.NEXT_PUBLIC_API_URL+"servEvents", {
        credentials: "include",
        mode: "cors"
    });

    return response.json();
}