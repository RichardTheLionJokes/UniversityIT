import { TicketRequest } from "@/app/services/helpdesk/tickets";
import Checkbox from "antd/es/checkbox/Checkbox";
import Input from "antd/es/input/Input";
import TextArea from "antd/es/input/TextArea";
import Modal from "antd/es/modal/Modal";
import { SetStateAction, useEffect, useState } from "react";

type Props = {
    mode: Mode;
    values: Ticket;
    isModalOpen: boolean;
    handleCancel: () => void;
    handleCreate: (request: TicketRequest) => void;
    handleUpdate: (id: string, request: TicketRequest) => void;
}

export enum Mode {
    Create,
    Edit
}

export const CreateUpdateTicket = ({
    mode,
    values,
    isModalOpen,
    handleCancel,
    handleCreate,
    handleUpdate,
}: Props) => {
    const [name, setName] = useState<string>("");
    const [description, setDescription] = useState<string>("");
    const [place, setPlace] = useState<string>("");
    const [isCompleted, setIsCompleted] = useState<boolean>(false);

    useEffect(() => {
        setName(values.name);
        setDescription(values.description);
        setPlace(values.place);
        setIsCompleted(values.isCompleted);
    }, [values]);

    const handleOnOk = async () => {
        const ticketRequest = { name, description, place, isCompleted };

        mode == Mode.Create 
            ? handleCreate(ticketRequest) 
            : handleUpdate(values.id, ticketRequest);
    }

    return (
        <Modal
            title={
                mode == Mode.Create ? "Add ticket" : "Edit ticket"
            }
            open={isModalOpen}
            cancelText="Cancel"
            onOk={handleOnOk}
            onCancel={handleCancel}
        >
            <div className="server__modal">
                <Input
                    value={name}
                    onChange={(e: { target: { value: SetStateAction<string>; }; }) => setName(e.target.value)}
                    placeholder = "Name"
                />
                <TextArea
                    value={description}
                    onChange={(e: { target: { value: SetStateAction<string>; }; }) => setDescription(e.target.value)}
                    autoSize={{ minRows: 3, maxRows: 3 }}
                    placeholder = "Description"
                />
                <Input
                    value={place}
                    onChange={(e: { target: { value: SetStateAction<string>; }; }) => setPlace(e.target.value)}
                    placeholder = "Place"
                />
                <Checkbox
                    checked={isCompleted}
                    onChange={(e: { target: { checked: boolean | ((prevState: boolean) => boolean); }; }) => setIsCompleted(e.target.checked)}
                >
                    Is completed
                </Checkbox>
                <p>{values.notificationsSent ? "✔" : "❌"} Notifications sent</p>
            </div>
        </Modal>
    )
}