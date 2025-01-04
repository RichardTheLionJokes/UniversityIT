import Button from "antd/es/button/button"
import { Space, Table, TableProps } from "antd";
import { FileImageTwoTone, FileOutlined, FilePdfTwoTone, FileWordTwoTone, FolderTwoTone } from '@ant-design/icons';

type Props = {
    fileStructures: FileStructure[];
    path: string;
    handleOpen: (id: number, isFolder: boolean) => void;
    handleEdit: (fileStructure: FileStructure) => void;
    handleDelete: (id: number, isFolder: boolean) => void;
}

export const FileStructures = ({ fileStructures, path, handleOpen, handleEdit, handleDelete }: Props) => {

    const columns: TableProps<FileStructure>['columns'] = [
        {
            title: "Name",
            key: "Ref",
            render: (_, record) => (
                record.isFolder ? (
                <Space size="small">
                    <FolderTwoTone twoToneColor="#FFD700" />                    
                    <a href={path + "?id=" + String(record.id)} style={{fontSize: "15px"}}>{record.name}</a>
                </Space>
                ) : (
                <Space size="small">
                    {(record.extention === ".pdf") ? (
                        <p>{record.extention}</p>
                    ) : ([".doc",".docx"].includes(record.extention) ? (
                        <FileWordTwoTone twoToneColor="#0000FF" />
                    ) : ([".jpg",".jpeg",".png",".img",".gif"].includes(record.extention) ? (
                        <FileImageTwoTone twoToneColor="#FF8C00" />
                    ) : (
                        <FileOutlined />
                    )))}
                    {record.name + record.extention}
                </Space>
                )
            ),
        },
        {
            title: "Action",
            key: "action",
            render: (_, record) => (
                <Space size="small">
                    <Button
                        onClick={() => handleEdit(record)}
                        style={{flex: 1}}
                    >
                        Edit
                    </Button>
                    <Button
                        onClick={() => handleDelete(record.id, record.isFolder)}
                        danger
                        style={{flex: 1}}
                    >
                        Delete
                    </Button>
                </Space>
            ),
        }
    ];

    return(
        <div>
            <Table
                columns={columns}
                dataSource={fileStructures}
                rowKey={(record) => record.id}
            />;
        </div>
    )
}