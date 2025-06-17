import { Table } from "antd";
import moment from "moment";

type EventRow = {
    key: number
    serverName: string
    serverIp: string
    servStatus: string
    happenedAt: Date
}

type Props = {
    servEvents: ServEvent[];
}

export const ServEvents = ({ servEvents }: Props) => {
    const dataSource: EventRow[] = [];

    const columns = [
        {
            title: "Server name",
            dataIndex: "serverName",
            key: "serverName",
        },
        {
            title: "IP-address",
            dataIndex: "serverIp",
            key: "serverIp",
        },
        {
            title: "Serv status",
            dataIndex: "servStatus",
            key: "servStatus",
        },
        {
            title: "Happened at",
            dataIndex: "happenedAt",
            key: "happenedAt",
            render: (value: Date) => { return <p>{moment(value).format("DD-MM-YYYY h:mm:ss")}</p> },
        },
      ];

    let i = 1;
    servEvents.map((servEvent: ServEvent) => {
        dataSource.push({
            key: i,
            serverName: servEvent.serverName,
            serverIp: servEvent.serverIp,
            servStatus: servEvent.servStatus,
            happenedAt: servEvent.happenedAt,
        });
        i = i + 1;
    });
    
    return(
        <div>
            <Table
                columns={columns}
                dataSource={dataSource}
            />;
        </div>
    )
}