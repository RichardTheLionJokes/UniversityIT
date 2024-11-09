import Button from "antd/es/button/button"
import { Space, Table, TableProps } from "antd";

type Props = {
    servers: Server[];
    handleOpen: (server: Server) => void;
    handlePing: (id: string) => void;
    handleDelete: (id: string) => void;
}

export const Servers = ({ servers, handleOpen, handlePing, handleDelete }: Props) => {
    const columns: TableProps<Server>['columns'] = [
        {
            title: "Name",
            dataIndex: "name",
            key: "name",
        },
        {
            title: "Ip address",
            dataIndex: "ipAddress",
            key: "ipAddress",
        },
        {
            title: "Short description",
            dataIndex: "shortDescription",
            key: "shortDescription",
        },
        {
            title: "Current status",
            dataIndex: "currentStatus",
            key: "currentStatus",
        },
        {
            title: "Activity",
            dataIndex: "activity",
            key: "activity",
            render: (value) => value == true ? "✔" : ""
        },
        {
            title: "Action",
            key: "action",
            render: (_, record) => (
                <Space size="small">
                    <Button
                        onClick={() => handleOpen(record)}
                        style={{flex: 1}}
                    >
                        Edit
                    </Button>
                    <Button
                        onClick={() => handlePing(record.id)}
                        style={{flex: 1}}
                    >
                        Ping
                    </Button>
                    <Button
                        onClick={() => handleDelete(record.id)}
                        danger
                        style={{flex: 1}}
                    >
                        Delete
                    </Button>
                </Space>
            ),
        }
    ];
    
    return (        
        <div>
            <Table
                columns={columns}
                dataSource={servers}
                rowKey={(record) => record.id}
            />;
        </div>
    )
}