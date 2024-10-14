import { ChangePasswordForm } from "@/app/components/auth/ChangePasswordForm";
import { authConfig } from "@/configs/auth";
import { getServerSession } from "next-auth";

export default async function ChangePassword() {
    const session = await getServerSession(authConfig);

    return (
        <div className="stack">
            <h1>Change password of {session?.user?.name}</h1>
            {session?.user?.email &&
                <ChangePasswordForm
                    email={session?.user?.email}
                />
            }
        </div>
    )
}