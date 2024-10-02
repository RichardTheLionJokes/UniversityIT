"use client"

import { signIn } from "next-auth/react"
import { useRouter, useSearchParams } from "next/navigation"
import type { FormEventHandler } from "react"
import { Login } from "@/app/services/auth/auth";

const SignInForm = () => {
    const router = useRouter();

    const searchParams = useSearchParams();
    const callbackUrl = searchParams?.get("callbackUrl") || "/profile";

    const handleSubmit: FormEventHandler<HTMLFormElement> = async (event) => {
        event.preventDefault();

        const formData = new FormData(event.currentTarget);
        const user = await Login(formData.get("email") as string, formData.get("password") as string);
        
        const res = await signIn("credentials", {
            email: user.email,
            name: user.name,
            redirect: false
        });

        if (res && !res.error) {
            router.push(callbackUrl);
        } else {
            console.log(res);
        }
    }

    return (
        <form onSubmit={handleSubmit} className="login-form">
            <input type="email" name="email" required />
            <input type="password" name="password" required />
            <button type="submit">Sign In</button>
        </form>
    )
}

export { SignInForm };