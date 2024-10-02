"use client";

import { ServEvents } from "@/app/components/serv_mon/ServEvents";
import { getAllServEvents } from "@/app/services/serv_mon/servEvents";
import Title from "antd/es/typography/Title";
import { useEffect, useState } from "react";

export default function ServEventsPage(){
    const [servEvents, setServEvents] = useState<ServEvent[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const getServEvents = async () => {
            const servers = await getAllServEvents();
            setLoading(false);
            setServEvents(servers);
        };

        getServEvents();
    }, []);

    return(
        <div>
            <Title>Events</Title>
            {loading ? (
                <Title>Loading...</Title>
            ) : (
                <ServEvents
                    servEvents={servEvents}
                />
            )}
        </div>
    )
}