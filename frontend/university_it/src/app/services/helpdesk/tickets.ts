export interface TicketRequest {
    name: string;
    description: string;
    place: string;
    isCompleted: boolean;
}

export const getTickets = async () => {
    const response = await fetch(process.env.NEXT_PUBLIC_API_URL+"tickets", {
        method: "GET",
        credentials: "include",
        mode: "cors"
    });

    return response.json();
}

export const createTicket = async (serverRequest: TicketRequest) => {
    await fetch(process.env.NEXT_PUBLIC_API_URL+"tickets", {
        method: "POST",
        headers: {
            "content-type": "application/json"
        },
        body: JSON.stringify(serverRequest),
        credentials: "include",
        mode: "cors"
    });
}

export const updateTicket = async (id: string, serverRequest: TicketRequest) => {
    await fetch(process.env.NEXT_PUBLIC_API_URL+`tickets/${id}`, {
        method: "PUT",
        headers: {
            "content-type": "application/json"
        },
        body: JSON.stringify(serverRequest),
        credentials: "include",
        mode: "cors"
    });
}

export const deleteTicket = async (id: string) => {
    await fetch(process.env.NEXT_PUBLIC_API_URL+`tickets/${id}`, {
        method: "DELETE",
        credentials: "include",
        mode: "cors"
    });
}