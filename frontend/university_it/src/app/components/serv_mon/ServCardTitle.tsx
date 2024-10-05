type Props = {
    name: string;
    ip_address: string;
}

export const ServCardTitle = ({name, ip_address}: Props) => {
    const sep = (name != "" && ip_address != "") ? <p>|</p> : "";

    return (
        <div style={{
            display: "flex",
            flexDirection: "row",
            alignItems: "center",
            justifyContent: "space-between"
        }}>
            <p className="card__title">{name}</p>
            {sep}            
            <p className="card__ip">{ip_address}</p>
        </div>
    )
}