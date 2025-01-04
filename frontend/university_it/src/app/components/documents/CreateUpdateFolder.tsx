import { FolderRequest } from "@/app/services/documents/folders";
import Input from "antd/es/input/Input";
import Modal from "antd/es/modal/Modal";
import { SetStateAction, useEffect, useState } from "react";

type Props = {
    mode: Mode;
    values: FileStructure;
    isModalOpen: boolean;
    handleCancel: () => void;
    handleCreate: (request: FolderRequest) => void;
    handleUpdate: (id: number, request: FolderRequest) => void;
}

export enum Mode {
    Create,
    Edit
}

export const CreateUpdateFolder = ({
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
        const folderRequest = { name, parentId };

        mode == Mode.Create 
            ? handleCreate(folderRequest) 
            : handleUpdate(values.id, folderRequest);
    }

    return (
        <Modal
            title={
                mode == Mode.Create ? "Add folder" : "Edit folder"
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
            </div>
        </Modal>
    )
}