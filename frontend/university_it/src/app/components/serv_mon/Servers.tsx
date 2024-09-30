import Card from "antd/es/card/Card"
import { ServCardTitle } from "./ServCardTitle"
import Button from "antd/es/button/button"

interface Props {
    servers: Server[];
    handleOpen: (server: Server) => void;
    handleDelete: (id: string) => void;
}

export const Servers = ({ servers, handleOpen, handleDelete }: Props) => {
    return (
        <div className="cards">
            {servers.map((server: Server) => (
                <Card
                    key={server.id}
                    title={<ServCardTitle name={server.name} ip_address={server.ipAddress}/>}
                    bordered={false}
                >
                    <p>{server.shortDescription}</p>
                    <p>{server.description}</p>
                    <p>{server.activity}</p>
                    <div className="card__buttons">
                        <Button
                            onClick={() => handleOpen(server)}
                            style={{flex: 1}}
                        >
                            Edit
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