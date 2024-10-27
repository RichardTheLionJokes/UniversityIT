"use client";

import { useEffect, useState } from "react";
import { createTicket, deleteTicket, getTickets, TicketRequest, updateTicket } from "../services/helpdesk/tickets";
import Title from "antd/es/typography/Title";
import { Tickets } from "../components/helpdesk/Tickets";
import Button from "antd/es/button/button";
import { CreateUpdateTicket, Mode } from "../components/helpdesk/CreateUpdateTicket";

export default function TicketsPage() {
    const defaultValues = {
        id: "",
        name: "",
        description: "",
        place: "",
        isCompleted: false
    } as Ticket;

    const [values, setValues] = useState<Ticket>(defaultValues);
    const [tickets, setTickets] = useState<Ticket[]>([]);
    const [loading, setLoading] = useState(true);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [mode, setMode] = useState(Mode.Create);

    useEffect(() => {
        const loadTickets = async () => {
            const tickets = await getTickets();
            setLoading(false);
            setTickets(tickets);
        };

        loadTickets();
    }, []);

    const handleCreateTicket = async (request: TicketRequest) => {
        await createTicket(request);
        closeModal();

        const tickets = await getTickets();
        setTickets(tickets);
    };

    const handleUpdateTicket = async (id: string, request: TicketRequest) => {
        await updateTicket(id, request);
        closeModal();

        const tickets = await getTickets();
        setTickets(tickets);
    };

    const handleDeleteTicket = async (id: string) => {
        await deleteTicket(id);
        closeModal();

        const tickets = await getTickets();
        setTickets(tickets);
    };

    const openModal = () => {
        setMode(Mode.Create);
        setIsModalOpen(true);
    };

    const closeModal = () => {
        setValues(defaultValues);
        setIsModalOpen(false);
    };

    const openEditModal = (ticket: Ticket) => {
        setMode(Mode.Edit);
        setValues(ticket);
        setIsModalOpen(true);
    };

    return(
        <div>
            <Title>HelpDesk</Title>
            <Button
                type="primary"
                style={{ marginTop: "30px"}}
                size="large"
                onClick={openModal}
            >
                Add ticket
            </Button>

            <CreateUpdateTicket
                mode={mode}
                values={values}
                isModalOpen={isModalOpen}
                handleCreate={handleCreateTicket}                
                handleUpdate={handleUpdateTicket}
                handleCancel={closeModal}
            />

            {loading ? (
                <Title>Loading...</Title>
            ) : (
                <Tickets
                    tickets={tickets}
                    handleOpen={openEditModal}
                    handleDelete={handleDeleteTicket}
                />
            )}
        </div>
    )
}