import { FileRequest } from "@/app/services/documents/files";
import Input from "antd/es/input/Input";
import Modal from "antd/es/modal/Modal";
import { SetStateAction, useEffect, useState } from "react";

type Props = {
    mode: Mode;
    values: FileStructure;
    isModalOpen: boolean;
    handleCancel: () => void;
    handleCreate: (request: FileRequest) => void;
    handleUpdate: (id: number, request: FileRequest) => void;
}

export enum Mode {
    Create,
    Edit
}

export const CreateUpdateFile = ({
    mode,
    values,
    isModalOpen,
    handleCancel,
    handleCreate,
    handleUpdate,
}: Props) => {
    const [name, setName] = useState<string>("");
    const [parentId, setParentId] = useState<number>(values.parentId);

    useEffect(() => {
        setName(values.name);
    }, [values]);

    const handleOnOk = async () => {
        const files = (document.querySelector('input[type="file"]') as HTMLInputElement | null)?.files;
        const file = (files != null) ? (files[0]) : undefined;
        const fileRequest = { name, parentId, file };

        mode == Mode.Create 
            ? handleCreate(fileRequest) 
            : handleUpdate(values.id, fileRequest);
    }

    return (
        <Modal
            title={
                mode == Mode.Create ? "Add file" : "Edit file"
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
                <input type="file" name="uploads" />
            </div>
        </Modal>
    )
}