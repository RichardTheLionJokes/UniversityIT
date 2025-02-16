import Button from "antd/es/button/button"
import { Space, Table, TableProps } from "antd";
import Icon, { DownloadOutlined, FileImageTwoTone, FileOutlined, FilePdfTwoTone, FileWordTwoTone } from '@ant-design/icons';
import { FolderIcon } from "./Icons";

type Props = {
    fileStructures: FileStructure[];
    path: string;
    handleDownloadRef: (id: number, isFolder: boolean) => string;
    handleEdit: (fileStructure: FileStructure) => void;
    handleDelete: (id: number, isFolder: boolean) => void;
}

export const FileStructures = ({ fileStructures, path, handleDownloadRef, handleEdit, handleDelete }: Props) => {

    const columns: TableProps<FileStructure>['columns'] = [
        {
            title: "Name",
            key: "Ref",
            render: (_, record) => (
                record.isFolder ? (
                <Space size="small">
                    <Icon component={FolderIcon} />
                    <a href={path + "?id=" + String(record.id)} style={{fontSize: "15px"}}>{record.name}</a>
                </Space>
                ) : (
                <Space size="small">
                    {(record.extention === ".pdf") ? (
                        <FilePdfTwoTone twoToneColor="#8B0000" style={{ fontSize: '16px' }} />
                    ) : ([".doc",".docx"].includes(record.extention) ? (
                        <FileWordTwoTone twoToneColor="#0000FF" style={{ fontSize: '16px' }} />
                    ) : ([".jpg",".jpeg",".png",".img",".gif"].includes(record.extention) ? (
                        <FileImageTwoTone twoToneColor="#FF8C00" style={{ fontSize: '16px' }} />
                    ) : (
                        <FileOutlined />
                    )))}
                    <a href={handleDownloadRef(record.id, record.isFolder)} target="_blank">{record.name + record.extention}</a>
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
                    {/*<Button
                        href={handleDownloadRef(record.id, record.isFolder)}
                        type="primary"
                        shape="round"
                        icon={<DownloadOutlined />}
                        style={{flex: 1}}
                    >
                        Download{record.isFolder ? ".zip" : ""}
                    </Button>*/}
                </Space>
            ),
        }
    ];

    return(
        <div>
            <Table
                size="small"
                columns={columns}
                dataSource={fileStructures}
                rowKey={(record) => record.isFolder ? "folder-" : "file-" + record.id}
            />;
        </div>
    )
}