import Card from "antd/es/card/Card"
import { ServCardTitle } from "./ServCardTitle"
import Button from "antd/es/button/button"

type Props = {
    servers: Server[];
    handleOpen: (server: Server) => void;
    handlePing: (id: string) => void;
    handleDelete: (id: string) => void;
}

export const Servers = ({ servers, handleOpen, handlePing, handleDelete }: Props) => {
    return (
        <div className="cards">
            {servers.map((server: Server) => (
                <Card
                    key={server.id}
                    title={<ServCardTitle name={server.name} ip_address={server.ipAddress}/>}
                    bordered={false}                    
                    style={{ backgroundColor: server.currentStatus == "Available" ? '#FFFFFF' : '#CCCCCC' }}
                >
                    <p>{server.shortDescription}</p>
                    <p>{server.currentStatus}</p>
                    <p>{server.activity}</p>
                    <div className="card__buttons">
                        <Button
                            onClick={() => handleOpen(server)}
                            style={{flex: 1}}
                        >
                            Edit
                        </Button>
                        <Button
                            onClick={() => handlePing(server.id)}
                            style={{flex: 1}}
                        >
                            Ping
                        </Button>
                        <Button
                            onClick={() => handleDelete(server.id)}
                            danger
                            style={{flex: 1}}
                        >
                            Delete
                        </Button>
                    </div>
                </Card>
            ))}
        </div>
    )
}