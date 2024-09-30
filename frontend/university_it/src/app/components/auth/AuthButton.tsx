"use client"

import { signIn } from "next-auth/react";
import { useSearchParams } from "next/navigation";

const AuthButton = () => {
    const searchParams = useSearchParams();
    const callbackUrl = searchParams?.get("callbackUrl") || "/profile";

    return (
        <div>
            <button onClick={() => signIn("credentials", {callbackUrl})}>
                Sign in
            </button>
        </div>
    )
}

export { AuthButton };