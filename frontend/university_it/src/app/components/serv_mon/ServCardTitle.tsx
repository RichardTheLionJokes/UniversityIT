type Props = {
    name: string;
    ip_address: string;
}

export const ServCardTitle = ({name, ip_address}: Props) => {
    return (
        <div style={{
            display: "flex",
            flexDirection: "row",
            alignItems: "center",
            justifyContent: "space-between"
        }}>
            <p className="card__title">{name}</p>
            <p className="card__price">{ip_address}</p>
        </div>
    )
}