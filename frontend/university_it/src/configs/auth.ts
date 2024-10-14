import type { AuthOptions, User } from "next-auth";
import Credentials from "next-auth/providers/credentials";

export const authConfig: AuthOptions = {
    providers: [
        Credentials({
            credentials: {
                email: { label: "email", type: "email", required: true },
                name: { label: "name", type: "name", required: true }
            },
            async authorize(credentials) {
                if (!credentials?.email || !credentials?.name) return null;

                authConfig.session = { maxAge: 24*60*60 }
                const currentUser = {email: credentials.email, name: credentials.name};
                return currentUser as User;
            }
        })
    ],
    pages: {
        signIn: "/signin"
    },
    secret: process.env.NEXTAUTH_SECRET
}