"use client"

import { Menu } from "antd";
import Link from "next/link";
import { useSession, signIn, signOut } from "next-auth/react";

type NavLink = {
    key: string
    label: string
    href: string
}

type Props = {
    navLinks: NavLink[];
}

const Navigation = ({ navLinks }: Props) => {
    const session = useSession();

    console.log(session);

    const items = 
        navLinks.map((link) => {
            return (
                {key: link.key, label: <Link href={link.href}>{link.label}</Link>}
            )
        });

    if (session?.data) items.push({key: "profile", label: <Link href="/profile">Profile</Link>});
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