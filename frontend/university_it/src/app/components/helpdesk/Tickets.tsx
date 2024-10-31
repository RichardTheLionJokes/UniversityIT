import { Space, Table, TableProps } from "antd";
import moment from "moment";

type Props = {
    tickets: Ticket[];
    handleOpen: (ticket: Ticket) => void;
    handleDelete: (id: string) => void;
}

export const Tickets = ({ tickets, handleOpen, handleDelete }: Props) => {
    const columns: TableProps<Ticket>['columns'] = [
        {
            title: "Name",
            dataIndex: "name",
            key: "name",
        },
        {
            title: "Place",
            dataIndex: "place",
            key: "place",
        },
        {
            title: "Author",
            dataIndex: "author",
            key: "author",
        },
        {
            title: "Created at",
            dataIndex: "createdAt",
            key: "createdAt",
            render: (value) => { return <p>{moment(value).format("DD-MM-YYYY h:mm:ss")}</p> },
        },
        {
            title: "Is completed",
            dataIndex: "isCompleted",
            key: "isCompleted",
            render: (value) => value == true ? "✔" : ""
        },
        {
            title: "Notifications sent",
            dataIndex: "notificationsSent",
            key: "notificationsSent",
            render: (value) => value == true ? "✔" : "❌"
        },
        {
            title: "Action",
            key: "action",
            render: (_, record) => (
                <Space size="middle">
                    <a onClick={() => handleOpen(record)}>Edit</a>
                    <a onClick={() => handleDelete(record.id)}>Delete</a>
                </Space>
            ),
        }
    ];

    return(
        <div>
            <Table
                columns={columns}
                dataSource={tickets}
                rowKey={(record) => record.id}
            />;
        </div>
    )
}