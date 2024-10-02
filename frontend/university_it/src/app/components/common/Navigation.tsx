"use client"

import { Menu } from "antd";
import Link from "next/link";
import { useSession, signIn, signOut } from "next-auth/react";
import { ItemType, MenuItemType } from "antd/es/menu/interface";

type Props = {
    navLinks: ItemType<MenuItemType>[];
}

const Navigation = ({ navLinks }: Props) => {
    const session = useSession();
    console.log(session);

    const items: ItemType<MenuItemType>[] = [];
    navLinks.map((link) => {
        items.push(link);
    })

    if (session?.data) {
        items.push({key: "serv_mon", label: "Server Monitoring",
            children: [
                {key: "servers", label: <Link href="/serv_mon/">Servers</Link>},
                {key: "servEvents", label: <Link href="/serv_mon/events">Events</Link>}]});
        items.push({key: "profile", label: <Link href="/profile">Profile</Link>});
    };
    items.push(session?.data ? (
        {key: "signout", label: <Link href="#" onClick={() => signOut({callbackUrl: "/"})}>Sign Out</Link>}
    ) : (
        {key: "signin", label: <Link href="/signin">Sign In</Link>}
    ));

    return (
        <Menu
            theme="dark"
            mode="horizontal"
            items={items}
            style={{ flex: 1, minWidth: 0 }}
        />
    );
}

export { Navigation }