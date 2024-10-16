"use client"

import { Menu } from "antd";
import Link from "next/link";
import { useSession, signOut } from "next-auth/react";
import { ItemType, MenuItemType } from "antd/es/menu/interface";
import { logout } from "@/app/services/auth/auth";

type Props = {
    navLinks: ItemType<MenuItemType>[];
}

const Navigation = ({ navLinks }: Props) => {
    const session = useSession();
    console.log(session);

    const SignOut = async () => {
        await logout();
        signOut({callbackUrl: "/"});
    }

    const items: ItemType<MenuItemType>[] = [];
    navLinks.map((link) => {
        items.push(link);
    })

    if (session?.data) {
        items.push({key: "serv_mon", label: "Server Monitoring",
            children: [
                {key: "servers", label: <Link href="/servers/">Servers</Link>},
                {key: "servEvents", label: <Link href="/servers/events">Events</Link>}]});
    };
    items.push(session?.data ? (
        {key: "profile1", label: session.data.user?.name,
            children: [
                {key: "profile", label: <Link href="/profile">Profile</Link>},
                {key: "signout", label: <Link href="#" onClick={() => SignOut()}>Sign Out</Link>}
            ]
        }
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