"use client";

import Button from "antd/es/button/button";
import { Servers } from "@/app/components/serv_mon/Servers";
import { useEffect, useState } from "react";
import { createServer, deleteServer, getAllServers, pingServer, ServerRequest, updateServer } from "@/app/services/serv_mon/servers";
import Title from "antd/es/typography/Title";
import { CreateUpdateServer, Mode } from "@/app/components/serv_mon/CreateUpdateServer";

export default function ServersPage(){
    const defaultValues = {
        name: "",
        ipAddress: "",
        shortDescription: "",
        description: "",
        activity: false
    } as Server;

    const [values, setValues] = useState<Server>(defaultValues);

    const [servers, setServers] = useState<Server[]>([]);
    const [loading, setLoading] = useState(true);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [mode, setMode] = useState(Mode.Create);

    useEffect(() => {
        const getServers = async () => {
            const servers = await getAllServers();
            setLoading(false);
            setServers(servers);
        };

        getServers();
    }, []);

    const handleCreateServer = async (request: ServerRequest) => {
        await createServer(request);
        closeModal();

        const servers = await getAllServers();
        setServers(servers);
    };

    const handleUpdateServer = async (id: string, request: ServerRequest) => {
        await updateServer(id, request);
        closeModal();

        const servers = await getAllServers();
        setServers(servers);
    };

    const handleDeleteServer = async (id: string) => {
        await deleteServer(id);
        closeModal();

        const servers = await getAllServers();
        setServers(servers);
    };

    const openModal = () => {
        setMode(Mode.Create);
        setIsModalOpen(true);
    };

    const closeModal = () => {
        setValues(defaultValues);
        setIsModalOpen(false);
    };

    const openEditModal = (server: Server) => {
        setMode(Mode.Edit);
        setValues(server);
        setIsModalOpen(true);
    };

    return(
        <div>
            <Button
                type="primary"
                style={{ marginTop: "30px"}}
                size="large"
                onClick={openModal}
            >
                Add server
            </Button>

            <CreateUpdateServer
                mode={mode}
                values={values}
                isModalOpen={isModalOpen}
                handleCreate={handleCreateServer}
                handleUpdate={handleUpdateServer}
                handleCancel={closeModal}
            />

            {loading ? (
                <Title>Loading...</Title>
            ) : (
                <Servers
                    servers={servers}
                    handleOpen={openEditModal}
                    handleDelete={handleDeleteServer}
                />
            )}
        </div>
    )
}