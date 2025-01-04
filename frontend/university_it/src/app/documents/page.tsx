"use client";

import Title from "antd/es/typography/Title";
import Button from "antd/es/button/button";
import { useEffect, useState } from "react";
import { FileStructures } from "../components/documents/FileStructures";
import { createFolder, FolderRequest, getFolderWithChilds, updateFolder } from "../services/documents/folders";
import { CreateUpdateFolder, Mode } from "../components/documents/CreateUpdateFolder";
import { FileRequest, uploadFile } from "../services/documents/files";
import { useSearchParams } from "next/navigation";
import { Space } from "antd";
import { CreateUpdateFile } from "../components/documents/CreateUpdateFile";

export default function DocumentsPage() {
    const searchParams = useSearchParams();
    const parId = Number(searchParams?.get("id")) || 1;

    const defaultValues = {
        id: 0,
        name: "",
        isFolder: true,
        parentId: parId
    } as FileStructure;

    const [loading, setLoading] = useState(true);
    const [values, setValues] = useState<FileStructure>(defaultValues);
    const [folderId, setFolderId] = useState(parId);
    const [folderName, setFolderName] = useState("");
    const [fileStructures, setFileStructures] = useState<FileStructure[]>([]);
    const [isModalFolderOpen, setIsModalFolderOpen] = useState(false);
    const [isModalFileOpen, setIsModalFileOpen] = useState(false);
    const [mode, setMode] = useState(Mode.Create);

    useEffect(() => {
        const loadChilds = async () => {
            const res:{id: number, name: string, parentId?: number, childs: FileStructure[]} = await getFolderWithChilds(folderId);
            setLoading(false);
            setFolderName(res.name);
            setFileStructures(res.childs);
        };

        loadChilds();
    }, []);

    const handleCreateFolder = async (request: FolderRequest) => {
        await createFolder(request);
        closeModal();

        const res:{id: number, name: string, parentId?: number, childs: FileStructure[]} = await getFolderWithChilds(folderId);
        setFileStructures(res.childs);
    };

    const handleCreateFile = async (request: FileRequest) => {
        await uploadFile(request);
        closeModal();

        const res:{id: number, name: string, parentId?: number, childs: FileStructure[]} = await getFolderWithChilds(folderId);
        setFileStructures(res.childs);
    };
    
    const handleOpenFileStructure = async (id: number, isFolder: boolean) => {
        
        if (isFolder)
        {
            //openFolder
        }
        else
        {
            //downloadFile
        }
    };
    
    const handleUpdateFolder = async (id: number, request: FolderRequest) => {
        await updateFolder(id, request);
        closeModal();

        const res:{id: number, name: string, parentId?: number, childs: FileStructure[]} = await getFolderWithChilds(folderId);
        setFileStructures(res.childs);
    };

    const handleUpdateFile = async (id: number, request: FileRequest) => {
        //
    };
    
    const handleDeleteFileStructure = async (id: number, isFolder: boolean) => {
        //
    };

    const openFolderModal = () => {
        setMode(Mode.Create);
        setIsModalFileOpen(false);
        setIsModalFolderOpen(true);
    };

    const openFileModal = () => {
        setMode(Mode.Create);
        setIsModalFolderOpen(false);
        setIsModalFileOpen(true);
    };

    const closeModal = () => {
        setValues(defaultValues);
        setIsModalFolderOpen(false);
        setIsModalFileOpen(false);
    };

    const openEditModal = (fileStructure: FileStructure) => {
        setMode(Mode.Edit);
        setValues(fileStructure);
        setIsModalFolderOpen(true);
    };

    return(
        <div>
            <Title>{folderName}</Title>
            <Space size="small">
                <Button
                    type="primary"
                    style={{ marginTop: "30px"}}
                    size="large"
                    onClick={openFolderModal}
                >
                    Add folder
                </Button>
                <Button
                    type="primary"
                    style={{ marginTop: "30px"}}
                    size="large"
                    onClick={openFileModal}
                >
                    Add file
                </Button>
            </Space>

            <CreateUpdateFolder
                mode={mode}
                values={values}
                isModalOpen={isModalFolderOpen}
                handleCreate={handleCreateFolder}                
                handleUpdate={handleUpdateFolder}
                handleCancel={closeModal}
            />

            <CreateUpdateFile
                mode={mode}
                values={values}
                isModalOpen={isModalFileOpen}
                handleCreate={handleCreateFile}                
                handleUpdate={handleUpdateFile}
                handleCancel={closeModal}
            />

            {loading ? (
                <Title>Loading...</Title>
            ) : (
                <FileStructures
                    fileStructures={fileStructures}
                    path="http://localhost:3000/documents"
                    handleOpen={handleOpenFileStructure}
                    handleEdit={openEditModal}
                    handleDelete={handleDeleteFileStructure}
                />
            )}
        </div>
    )
}