import { authConfig } from "@/configs/auth";
import { getServerSession } from "next-auth/next";
import { ProfileForm } from "../components/auth/ProfileForm";

export default async function Profile() {
    const session = await getServerSession(authConfig);

    return (
        <div className="stack">
            <h1>Profile of {session?.user?.name}</h1>
            {session?.user?.image && <img src={session.user.image} alt=""/>}
            {session?.user?.email &&
                <ProfileForm
                    email={session?.user?.email}
                />
            }
        </div>
    );
}