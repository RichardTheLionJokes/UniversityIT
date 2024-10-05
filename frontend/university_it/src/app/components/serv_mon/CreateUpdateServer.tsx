import { ServerRequest } from "@/app/services/serv_mon/servers";
import Checkbox from "antd/es/checkbox/Checkbox";
import Input from "antd/es/input/Input";
import TextArea from "antd/es/input/TextArea";
import Modal from "antd/es/modal/Modal";
import { SetStateAction, useEffect, useState } from "react";

type Props = {
    mode: Mode;
    values: Server;
    isModalOpen: boolean;
    handleCancel: () => void;
    handleCreate: (request: ServerRequest) => void;
    handleUpdate: (id: string, request: ServerRequest) => void;
}

export enum Mode {
    Create,
    Edit
}

export const CreateUpdateServer = ({
    mode,
    values,
    isModalOpen,
    handleCancel,
    handleCreate,
    handleUpdate,
}: Props) => {
    const [name, setName] = useState<string>("");
    const [ipAddress, setIpAddress] = useState<string>("");
    const [shortDescription, setShortDescription] = useState<string>("");
    const [description, setDescription] = useState<string>("");
    const [activity, setActivity] = useState<boolean>(false);

    useEffect(() => {
        setName(values.name);
        setIpAddress(values.ipAddress);
        setShortDescription(values.shortDescription);
        setDescription(values.description);
        setActivity(values.activity);
    }, [values]);

    const handleOnOk = async () => {
        const serverRequest = { name, ipAddress, shortDescription, description, activity };

        mode == Mode.Create 
            ? handleCreate(serverRequest) 
            : handleUpdate(values.id, serverRequest);
    }

    return (
        <Modal
            title={
                mode == Mode.Create ? "Add server" : "Edit server"
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
                <Input
                    value={ipAddress}
                    onChange={(e: { target: { value: SetStateAction<string>; }; }) => setIpAddress(e.target.value)}
                    placeholder = "Ip-address"
                />
                <TextArea
                    value={shortDescription}
                    onChange={(e: { target: { value: SetStateAction<string>; }; }) => setShortDescription(e.target.value)}
                    autoSize={{ minRows: 3, maxRows: 3 }}
                    placeholder = "Short description"
                />
                <TextArea
                    value={description}
                    onChange={(e: { target: { value: SetStateAction<string>; }; }) => setDescription(e.target.value)}
                    autoSize={{ minRows: 3, maxRows: 3 }}
                    placeholder = "Description"
                />
                <h3>Status: {values.currentStatus}</h3>
                <Checkbox
                    checked={activity}
                    onChange={(e: { target: { checked: boolean | ((prevState: boolean) => boolean); }; }) => setActivity(e.target.checked)}
                >
                    Activity
                </Checkbox>
            </div>
        </Modal>
    )
}