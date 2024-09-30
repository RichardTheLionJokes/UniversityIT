import { SignInForm } from "@/app/components/auth/SignInForm";

export default async function Signin() {
    return (
        <div className="stack">
            <h1>Sign In</h1>
            <SignInForm />
        </div>
    )
}